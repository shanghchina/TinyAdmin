<!--提取器处理-提取器处理规则页面-->
<template>
  <div class="dashboard-container">
    <div class="el-col el-col-sm-24 el-col-md-24 el-col-lg-24 poc_content_tit">提取器处理</div>
    <div class="poc_add_container">
      <div class="step_panel">
        <el-steps :active="active">
          <el-step title="提取规则" />
          <el-step title="产品提取" />
          <el-step title="提取确认" />
          <el-step title="提取完成" />
        </el-steps>
      </div>
      <div>
        <p>产品数：{{ totalCount }}条</p>
        <div class="tag_box">已提取：{{ multipleSelection.length }}条
          <el-tag
            v-for="(tag,index) in multipleSelection"
            :key="index"
            closable
            :disable-transitions="false"
            @close="handleClose(tag)"
          >
            {{ tag.CourseName }}
          </el-tag>
        </div>
        <el-table
          ref="multipleTable"
          :data="tableData"
          tooltip-effect="dark"
          style="width: 100%"
          :row-key="getRowKeys"
          border
          @selection-change="handleSelectionChange"
        >
          <el-table-column
            :reserve-selection="true"
            type="selection"
            width="55"
          />
          <el-table-column
            prop="POCSource"
            label="系统来源"
          >
            <template slot-scope="scope">
              {{ scope.row.POCSource===100?'校管家':'' }}
            </template>
          </el-table-column>
          <el-table-column
            prop="TeachLevelOneOrgName"
            label="所属事业部"
            show-overflow-tooltip
            width="150"
          />
          <el-table-column
            prop="CourseName"
            label="课程名称"
            width="150"
          />
          <el-table-column
            prop="ProductTypeName"
            label="所属分类"
            width="150"
          />
          <el-table-column
            prop="CourseStatusName"
            label="课程状态"
          />
          <el-table-column
            prop="FeeUnitPrice"
            label="课时单价"
          />
          <el-table-column
            prop="FeeUnitPriceName"
            label="课时单位"
          />
          <el-table-column
            prop="TotalClassHour"
            label="课时数"
          />
          <el-table-column
            prop="CourseYear"
            label="所属年份"
          />
          <el-table-column
            prop="GradeName"
            label="所属年级"
          />
          <el-table-column
            prop="CategoryName"
            label="所属类型"
          />
          <el-table-column
            prop="SubjectName"
            label="所属科目"
          />
          <el-table-column
            prop="TermName"
            label="所属期段"
          />
          <el-table-column
            prop="ClassTypeName"
            label="所属班型"
          />
          <el-table-column
            width="120"
            property="CampusCountName"
            label="授权校区(个)"
          />
          <el-table-column prop="ProductCreatedDate" label="创建日期" width="200">
            <template slot-scope="scope">
              {{ scope.row.ProductCreatedDate | formatDate('yyyy-MM-dd hh:mm:ss') }}
            </template>
          </el-table-column>
          <el-table-column prop="ProductUpdateDate" label="更新日期" width="200">
            <template slot-scope="scope">
              {{ scope.row.ProductUpdateDate | formatDate('yyyy-MM-dd hh:mm:ss') }}
            </template>
          </el-table-column>
        </el-table>
        <el-pagination
          :current-page="condition.pageIndex"
          :page-sizes="[10, 20, 30, 40]"
          :page-size="condition.pageSize"
          layout="total, sizes, prev, pager, next, jumper"
          :total="totalCount"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
        />
      </div>
      <div class="el-col el-col-sm-24 el-col-md-24 el-col-lg-24" style="text-align: center;margin-bottom: 100px">
        <el-button type="" size="medium" @click="onPrevSubmit">上一步</el-button>
        <el-button type="primary" size="medium" @click="onSubmit">下一步</el-button>
        <el-button size="medium" @click="cancle">取消</el-button>
      </div>
    </div>
  </div>
</template>
<script>
import { showLoading, hideLoading } from '@/utils/loading'
import api from '@/api/poc_extractor'
import common from '@/utils/common'

export default {
  data() {
    return {
      getRowKeys(row) {
        return row.CourseID
      },
      active: 2,
      dynamicTags: [],
      totalCount: 0, // 数据总数
      multipleSelection: [], // 当前页选中的数据
      tableData: [],
      condition: { pageIndex: 1, pageSize: 10 }
    }
  },
  created: function() {
    this.condition = JSON.parse(sessionStorage.getItem('condition')) || {}
    var that = this
    that.queryData()
    var products = JSON.parse(sessionStorage.getItem('products')) || []
    if (products.length > 0) {
      this.$nextTick(function() {
        products.forEach(function(item) {
          that.$refs.multipleTable.toggleRowSelection(item)
        })
      })
    }
  },
  methods: {
    handleClose(row) {
      var that = this
      this.$nextTick(function() {
        that.tableData.forEach(function(item) {
          if (item.CourseID === row.CourseID) {
            that.$refs.multipleTable.toggleRowSelection(item)
          }
        })
      })
    },
    handleSelectionChange(val) {
      this.multipleSelection = val
    },
    handleSizeChange(val) {
      this.condition.pageSize = val
      this.condition.pageIndex = 1
      this.queryData()
    },
    handleCurrentChange(val) {
      this.condition.pageIndex = val
      this.queryData()
    },
    onPrevSubmit() {
      if (this.multipleSelection.length > 0) {
        sessionStorage.setItem('products', JSON.stringify(this.multipleSelection))
      }
      this.$router.push({ path: '/ext_product_pool/poc_extractor_step01', query: {
        id: this.$route.query.id
      }})
    },
    onSubmit() {
      if (this.multipleSelection.length > 0) {
        sessionStorage.setItem('products', JSON.stringify(this.multipleSelection))
        this.$router.push({ path: '/ext_product_pool/poc_extractor_step03', query: {
          id: this.$route.query.id
        }})
      } else {
        common.toast('请选择要提取的产品', 'error')
      }
    },
    cancle() {
      this.$router.push({ path: '/ext_product_pool/poc_extractor' })
    },

    async queryData() {
      showLoading()
      var request = this.condition
      var res = await api.getCouseListByExtractor(request)
      if (res.Code === '0') {
        this.totalCount = res.Data.totalCount
        this.tableData = res.Data.dataList
      }
      hideLoading()
    } }

}
</script>
<style lang="scss" scoped>
  .dashboard {
    &-container {
      margin: 30px 30px 0 30px;
    }
    &-text {
      font-size: 30px;
      line-height: 46px;
    }
  }
  .poc_content_tit{
    border-left: 3px solid #409eff;
    padding-left: 10px;
    color: #409eff;
    font-size: 16px;
  }

  .poc_add_container .step_panel{
    width: 100%;
    clear: both;
    margin-top: 60px;
    margin-bottom: 100px;
    float: left;
  }
  .poc_add_container{
    width: 70%;
    margin: 0 auto;
  }
  .poc_add_container .el-tag{
    margin-right: 10px;
    margin-bottom: 10px;
  }
  .tag_box{
    margin-bottom: 20px;
  }
  .el-pagination{
    margin-bottom: 30px;
  }
</style>

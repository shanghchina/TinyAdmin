<!--提取器处理-提取器确认页面-->
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
        <p class="span_box"><span>产品数：<em>{{ totalCount+extractorTable.length }}条</em></span>
          <span>提取数：<em>{{ extractorTable.length }}条</em></span>
          <span>未提取数：<em>{{ totalCount }}条</em></span>
        </p>
        <div class="table_box">
          <div class="table_tit_box">
            提取产品预览
          </div>

          <el-table
            ref="multipleExtractorTable"
            :data="extractorTable"
            tooltip-effect="dark"
            style="width: 100%"
            border
          >

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
              property="CampusCountName"
              label="授权校区(个)"
              width="150"
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
        </div>
        <div class="table_box">
          <div class="table_tit_box">
            未提取产品预览
          </div>
          <div class="btn_box">
            <el-button type="success" @click="HandleRelateClick">关联提取产品</el-button>
            <el-button type="success" @click="HandleClearClick">清除关联</el-button>
          </div>
          <el-table
            ref="multipleTable"
            :data="notExtractorTable"
            tooltip-effect="dark"
            style="width: 100%"
            :row-key="getRowKeys"
            border
            @selection-change="handleSelectionChange"
          >
            <el-table-column
              type="selection"
              :reserve-selection="true"
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
              prop="relationCouseName"
              label="关联产品"
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
              property="CampusCountName"
              label="授权校区(个)"
              width="150"
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
      </div>
      <el-dialog title="关联产品" :visible.sync="dialogTableVisible" :show-close="false">
        <el-table :data="extractorTable" border @row-click="showRow">
          <el-table-column label="" width="50">
            <template slot-scope="scope">
              <el-radio v-model="rowIndex" class="radio" :label="scope.$index">&nbsp;</el-radio>
            </template>
          </el-table-column>
          <el-table-column
            prop="POCSource"
            label="系统来源"
          >
            <template slot-scope="scope">
              {{ scope.row.POCSource===100?'校管家':'' }}
            </template>
          </el-table-column>
          <el-table-column prop="TeachLevelOneOrgName" label="所属事业部" width="200" />
          <el-table-column prop="CourseName" label="课程名称" />

          <el-table-column prop="ProductTypeName" label="所属分类" />
          <el-table-column prop="CourseStatusName" label="课程状态" />
          <el-table-column prop="FeeUnitPrice" label="课时单价" />
        </el-table>
        <div slot="footer" class="dialog-footer" style="text-align: center">

          <el-button type="primary" @click="handleSelectClick">选择</el-button>
          <el-button @click="cancleClick">取 消</el-button>
        </div>
      </el-dialog>
      <div class="el-col el-col-sm-24 el-col-md-24 el-col-lg-24" style="text-align: center;margin-bottom: 100px">
        <el-button type="" size="medium" @click="onPrevSubmit">上一步</el-button>
        <el-button type="primary" size="medium" @click="onSubmit">提交</el-button>
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
      active: 3,
      extractorTable: [],
      notExtractorTable: [],
      condition: { pageIndex: 1, pageSize: 10 },
      totalCount: 0,
      dialogTableVisible: false,
      multipleSelection: [],
      rowIndex: -1,
      selectedRow: {},
      // 关联关系
      relationArr: []

    }
  },
  mounted: function() {
    this.condition = JSON.parse(sessionStorage.getItem('condition')) || {}
    this.extractorTable = JSON.parse(sessionStorage.getItem('products')) || []
    this.relationArr = JSON.parse(sessionStorage.getItem('relationArr')) || []
    this.queryData()
  },
  methods: {
    handleSizeChange(val) {
      this.condition.pageSize = val
      this.condition.pageIndex = 1
      this.queryData()
    },
    handleCurrentChange(val) {
      this.condition.pageIndex = val
      this.queryData()
    },
    // 关联按钮
    HandleRelateClick() {
      if (this.multipleSelection.length > 0) {
        this.dialogTableVisible = true
      } else {
        common.toast('请选择要关联的产品', 'error')
      }
    },

    // 关联确认
    handleSelectClick() {
      var that = this
      if (this.rowIndex >= 0) {
        this.selectedRow = this.extractorTable[ this.rowIndex ]
        var retaionArr01 = []
        this.multipleSelection.forEach(function(item, index) {
          retaionArr01.push({ relationCouseId: that.selectedRow.CourseID, relationCouseName: that.selectedRow.CourseName, reCourseId: item.CourseID })
        })
        // 合并数组
        let length1 = that.relationArr.length
        const length2 = retaionArr01.length
        for (let i = 0; i < length1; i++) {
          for (let j = 0; j < length2; j++) {
            // 判断添加的数组是否为空了
            if (that.relationArr.length > 0) {
              if (that.relationArr[i]['reCourseId'] === retaionArr01[j]['reCourseId']) {
                that.relationArr.splice(i, 1) // 利用splice函数删除元素，从第i个位置，截取长度为1的元素
                length1--
              }
            }
          }
        }
        for (let n = 0; n < retaionArr01.length; n++) {
          that.relationArr.push(retaionArr01[n])
        }
        that.notExtractorTable.forEach(function(item) {
          var arr = that.relationArr.filter((x) => { return x.reCourseId === item.CourseID })
          if (arr.length > 0) {
            item.relationCouseId = arr[0].relationCouseId
            item.relationCouseName = arr[0].relationCouseName
          }
        })
        this.dialogTableVisible = false
        this.rowIndex = -1
        this.multipleSelection.forEach(function(row) {
          that.$refs.multipleTable.toggleRowSelection(row)
        })
        this.multipleSelection = []
      } else {
        common.toast('请选择要关联的产品', 'error')
      }
    },
    // 清除关联
    HandleClearClick() {
      var that = this
      if (this.multipleSelection.length > 0) {
        let length1 = that.relationArr.length
        const length2 = that.multipleSelection.length
        for (let i = 0; i < length1; i++) {
          for (let j = 0; j < length2; j++) {
            // 判断添加的数组是否为空了
            if (that.relationArr.length > 0) {
              if (that.relationArr[i]['reCourseId'] === that.multipleSelection[j]['CourseID']) {
                that.relationArr.splice(i, 1) // 利用splice函数删除元素，从第i个位置，截取长度为1的元素
                length1--
              }
            }
          }
        }
        that.notExtractorTable.forEach(function(item) {
          var arr = that.relationArr.filter((x) => { return x.reCourseId === item.CourseID })
          if (arr.length > 0) {
            item.relationCouseId = arr[0].relationCouseId
            item.relationCouseName = arr[0].relationCouseName
          } else {
            item.relationCouseId = ''
            item.relationCouseName = ''
          }
        })
        this.multipleSelection.forEach(function(row) {
          that.$refs.multipleTable.toggleRowSelection(row)
        })
      } else {
        common.toast('请选择产品', 'error')
      }
    },
    // 关联取消
    cancleClick() {
      this.dialogTableVisible = false
      this.rowIndex = -1
      this.selectedRow = {}
    },
    showRow(row) {
      event.preventDefault()
      this.rowIndex = this.extractorTable.indexOf(row)
    },
    onPrevSubmit() {
      if (this.relationArr.length > 0) {
        sessionStorage.setItem('relationArr', JSON.stringify(this.relationArr))
      }
      this.$router.push({ path: '/ext_product_pool/poc_extractor_step02', query: {
        id: this.$route.query.id
      }})
    },
    onSubmit() {
      showLoading()
      var that = this
      // 提交
      var request = { productList: [], totalCount: that.extractorTable.length + that.totalCount }
      var extractorCondition = JSON.parse(sessionStorage.getItem('extractorCondition'))
      this.extractorTable.forEach(function(item) {
        const productItem = {}
        productItem.FKDetailGuid = that.$route.query.id
        productItem.ProductGuid = '00000000-0000-0000-0000-000000000000'
        productItem.ProductNo = ''
        productItem.ProductTypeGuid = '00000000-0000-0000-0000-000000000000'
        productItem.ProductTypeName = ''
        productItem.ProductTypeLable = ''
        productItem.FromSystem = item.FromSystem
        productItem.TeachLevelOneOrgID = item.TeachLevelOneOrgID
        productItem.TeachLevelOneOrgName = item.TeachLevelOneOrgName
        productItem.CourseID = item.CourseID
        productItem.CourseName = item.CourseName
        productItem.ProductTypeOneID = item.ProductTypeOneID
        productItem.ProductTypeTwoID = item.ProductTypeTwoID
        productItem.CourseYear = item.CourseYear
        productItem.TermID = item.TermID
        productItem.TermName = item.TermName
        productItem.GradeID = item.GradeID
        productItem.GradeName = item.GradeName
        productItem.ClassTypeID = item.ClassTypeID
        productItem.ClassTypeName = item.ClassTypeName
        productItem.FlagID = item.FlagID
        productItem.FlagName = item.FlagName
        productItem.SubjectID = item.SubjectID
        productItem.SubjectName = item.SubjectName
        productItem.CategoryID = item.CategoryID
        productItem.CategoryName = item.CategoryName
        productItem.TotalClassHour = item.TotalClassHour
        productItem.TotalClassHourName = item.TotalClassHourName
        productItem.FeeUnitPrice = item.FeeUnitPrice
        productItem.FeeUnitPriceName = item.FeeUnitPriceName
        productItem.CourseStatus = item.CourseStatus
        productItem.ProductCreatedDate = item.ProductCreatedDate
        productItem.ProductUpdateDate = item.ProductUpdateDate
        productItem.GradeDictGuid = extractorCondition.GradeDictGuid
        productItem.GradeDictName = extractorCondition.GradeDictName
        productItem.CategoryDictGuid = extractorCondition.CategoryDictGuid
        productItem.CategoryDictName = extractorCondition.CategoryDictName
        productItem.SubjectDictGuid = extractorCondition.SubjectDictGuid
        productItem.SubjectDictName = extractorCondition.SubjectDictName
        productItem.TermDictGuid = extractorCondition.TermDictGuid
        productItem.TermDictName = extractorCondition.TermDictName
        productItem.ClassTypeDictGuid = extractorCondition.ClassTypeDictGuid
        productItem.ClassTypeDictName = extractorCondition.ClassTypeDictName
        productItem.RelationIds = that.relationArr.filter((ele) => { return ele.relationCouseId === productItem.CourseID }).map(function(elem, index) { return elem.reCourseId })
        productItem.Describe = ''
        productItem.CampusCountName = item.CampusCountName
        request.productList.push(productItem)
      })
      api.extractProducts(request).then(function(res) {
        hideLoading()
        if (res.Code === '0') {
          that.$router.push({ path: '/ext_product_pool/poc_extractor_step04' })
        } else {
          common.toast('提交失败', 'error')
        }
      })
    },
    handleSelectionChange(val) {
      this.multipleSelection = val
    },

    async queryData() {
      showLoading()
      var that = this
      var request = this.condition
      request.notInCourseGuid = this.extractorTable.map(function(elem, index) { return elem.CourseID })
      var res = await api.getCouseListByExtractor(request)
      if (res.Code === '0') {
        this.totalCount = res.Data.totalCount
        res.Data.dataList.forEach(function(tiem) {
          tiem.relationCouseId = ''
          tiem.relationCouseName = ''
        })
        this.notExtractorTable = res.Data.dataList
        that.notExtractorTable.forEach(function(item) {
          var arr = that.relationArr.filter((x) => { return x.reCourseId === item.CourseID })
          if (arr.length > 0) {
            item.relationCouseId = arr[0].relationCouseId
            item.relationCouseName = arr[0].relationCouseName
          }
        })
      }
      hideLoading()
    },
    cancle() {
      this.$router.push({ path: '/ext_product_pool/poc_extractor' })
    }
  }
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
  .table_tit_box{
    border-left: 3px solid #409eff;
    padding-left: 10px;
    color: #409eff;
    font-size: 16px;
    margin-bottom: 10px;
    margin-top: 30px;
  }
  .table_box{
    margin-bottom: 50px;
  }
  .table_box:last-child{
    margin-bottom: 0;
  }
  .btn_box{
    margin: 20px 0;
  }
  .span_box em{
    color: red;
    font-style: normal;
  }
  .el-pagination{
    margin-bottom: 30px;
  }
</style>

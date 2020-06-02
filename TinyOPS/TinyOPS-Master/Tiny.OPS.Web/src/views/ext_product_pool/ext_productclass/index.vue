<!--产品分类页面-->
<template>
  <div class="dashboard-container">
    <el-col :span="24">
      <el-form :inline="true" :model="formOptions" class="demo-form-inline">
        <SystemSource @childFn="getSystemSourceData" />
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="班级状态">
            <el-select v-model="formOptions.ClassStatus" placeholder="请选择" collapse-tags>
              <el-option label="全部" value="2" />
              <el-option label="有效" value="1" />
              <el-option label="无效" value="0" />
            </el-select>
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="班级名称">
            <el-input v-model="formOptions.ClassName" />
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item>
            <el-button type="primary" v-if="isExitPermissionPoint('EXT_ProductClass_Query')" @click="onDoQuery">查询</el-button>
            <el-button type="primary" v-if="isExitPermissionPoint('EXT_ProductClass_Export')" @click="onExport">导出</el-button>
          </el-form-item>
        </div>
      </el-form>
      <el-table :data="classTable.tableData" border style="width: 100%" empty-text=" ">
        <el-table-column prop="POCSource" label="系统来源">
          <template slot-scope="scope">
            <span>{{ scope.row.POCSource=='100'? '校管家':'' }}</span>
          </template>
        </el-table-column>
        <el-table-column prop="TeachLevelOneOrgName" label="所属事业部" width="120"/>
        <el-table-column prop="TeachNetOrgName" label="末级组织" />
        <el-table-column prop="ClassName" label="班级名称" />
        <el-table-column prop="CourseName" label="所属课程" />
        <el-table-column prop="TeacherUserName" label="上课老师" />
        <el-table-column prop="MaxStudentAmoun" label="人数" />
        <el-table-column prop="OpenDate" label="计划开班日期" width="125">
          <template slot-scope="scope">
            <span>{{ scope.row.OpenDate |formatDate('yyyy-MM-dd hh:mm:ss') }}</span>
          </template>
        </el-table-column>
        <el-table-column prop="CloseDate" label="计划结业时期" width="125">
          <template slot-scope="scope">
            <span>{{ scope.row.CloseDate |formatDate('yyyy-MM-dd hh:mm:ss') }}</span>
          </template>
        </el-table-column>
        <el-table-column prop="TotalClassHour" label="计划总课时数" width="125"/>
        <el-table-column prop="TotalClassHourName" label="总课时数的单位" width="125"/>
        <el-table-column prop="ProductUpdateDate" label="更新日期">
          <template slot-scope="scope">
            <span>{{ scope.row.ProductUpdateDate |formatDate('yyyy-MM-dd hh:mm:ss') }}</span>
          </template>
        </el-table-column>
      </el-table>
      <div class="paginationBox">
      <el-pagination
        :current-page="classTable.currentPage"
        :total="classTable.pageTotal"
        :page-sizes="[10, 20, 30, 40]"
        :page-size="classTable.pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        @current-change="handleCurrentChange"
        @size-change="handleSizeChange"
      />
      </div>
    </el-col>
  </div>
</template>
<script>
import SystemSource from '@/components/SystemSource'
import * as api from '../../../api/ext_productclass'
import * as Loading from '@/utils/loading'
export default {
  components: {
    SystemSource
  },
  data() {
    return {
      formOptions: {
        PocSource: [], // 系统来源
        LevelOneOrgID: [], // 所属事业部
        ClassStatus: '', // 班级状态
        ClassName: '', // 班级名称
        PageSize: 10,
        PageIndex: 1
      },
      classTable: {
        tableData: [],
        currentPage: 1,
        pageTotal: 0,
        pageSizes: [10, 20, 30, 40],
        pageSize: 10
      }
    }
  },
  created() {

  },
  methods: {
    getParameter() {
      const parameter = {
        PocSource: this.formOptions.PocSource,
        LevelOneOrgID: this.formOptions.LevelOneOrgID,
        ClassStatus: this.formOptions.ClassStatus,
        ClassName: this.formOptions.ClassName,
        PageIndex: this.classTable.currentPage,
        PageSize: this.classTable.pagesize
      }
      return parameter
    },
    onDoQuery() {
      console.log('onDoQuery!')
      if (this.formOptions.PocSource.length <= 0) {
        this.common.toast('系统来源必填', 'error')
      } else {
        this.classTable.currentPage = 1
        this.getEXTClassByPageList(1)
        console.log('onDoQuery successed!')
      }
    },
    handleSizeChange(val) {
        if (this.formOptions.PocSource.length <= 0) {
        this.common.toast('系统来源必填', 'error')
      } else {
        this.pagesize = val
        this.classTable.currentPage = 1
        this.formOptions.PageSize = val
        this.getEXTClassByPageList(this.classTable.currentPage)
      }
    },
    handleCurrentChange(val) {
        if (this.formOptions.PocSource.length <= 0) {
        this.common.toast('系统来源必填', 'error')
      } else {
        this.classTable.currentPage = val
        this.getEXTClassByPageList(val)
      }
    },
    // 获取班级信息
    async getEXTClassByPageList(currentPageIndex) {
      try {
        console.log('begin do getEXTClassByPageList......')
        Loading.showLoading()
        this.formOptions.PageIndex = currentPageIndex
        // this.formOptions.PageSize = this.pagesize
        // this.formOptions.ClassName =
        console.log(this.formOptions)
        const data = await api.getEXTClassByPageList(this.formOptions)
        if (data.Code === '0') {
          console.log('getEXTClassByPageList success')
          this.classTable.tableData = data.Data.ReusltList
          this.classTable.pageTotal = data.Data.RecordCount
          console.log(this.classTable.tableData)
        }
      } catch (e) {
        console.log(e)
      }
      Loading.hideLoading()
    },
    // 导出excel
    async onExport() {
      console.log('onExport!')
      if (this.formOptions.PocSource.length > 0) {
        this.classTable.currentPage = 1
        const parameter = this.getParameter()
        parameter.IsExport = true
        debugger
        const courseData = await api.exportExcle(parameter)
        var downloadElement = document.createElement('a')
        var href = window.URL.createObjectURL(courseData) // 创建下载的链接
        downloadElement.href = href
        downloadElement.download = '班级信息.xls' // 下载后文件名
        document.body.appendChild(downloadElement)
        downloadElement.click() // 点击下载
        document.body.removeChild(downloadElement) // 下载完成移除元素
        window.URL.revokeObjectURL(href) // 释放掉blob对象
      } else {
        this.common.toast('系统来源必填', 'error')
      }
    },
    getSystemSourceData(val) {
      debugger
      this.formOptions.PocSource = val.pocSource
      this.formOptions.LevelOneOrgID = val.oneOrgId
    }
  }

}</script>
<style lang="scss" scoped>

  .dashboard {
    &-container

  {
    margin: 30px;
  }

  &-text {
    font-size: 30px;
    line-height: 46px;
  }
  }
  .demo-form-inline >>> .el-form-item__label{
    width: 100px;
    text-align: left;
  }
  @media only screen and (max-width: 1508px){
    .demo-form-inline >>> .el-form-item__label{
      width: 100%;
      text-align: left;
    }
  }
  .paginationBox{
    float: left;
    width: 100%;
  }
</style>

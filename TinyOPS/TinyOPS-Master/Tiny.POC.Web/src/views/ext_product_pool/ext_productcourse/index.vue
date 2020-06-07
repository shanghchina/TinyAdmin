<template>
  <div class="dashboard-container">
    <el-col :span="24">
      <el-form :inline="true" :model="formOptions" class="productCourseBox">
        <SystemSource @childFn="getSystemSourceData" />
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="课程状态">
            <el-select v-model="formOptions.courseStatus" placeholder="请选择" collapse-tags>
              <el-option label="全部" value="-2" />
              <el-option label="有效" value="1" />
              <el-option label="无效" value="0" />
            </el-select>
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="提取状态">
            <el-select v-model="formOptions.extractStatus" placeholder="请选择" collapse-tags>
              <el-option label="未提取" value="1000" />
              <el-option label="已提取" value="2000" />
              <el-option label="已关联" value="3000" />
            </el-select>
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="课程名称">
            <el-input v-model="formOptions.courseName" />
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6 marginTop">
          <el-form-item>
            <el-button v-if="isExitPermissionPoint('EXT_ProductCourse_Query')" type="primary" @click="onSubmit">查询</el-button>
            <el-button v-if="isExitPermissionPoint('EXT_ProductCourse_Export')" type="primary" @click="onExport">导出</el-button>
          </el-form-item>
        </div>
      </el-form>
      <el-table :data="tableData" border style="width: 100%" empty-text=" ">
        <el-table-column prop="PocSource" label="系统来源" />
        <el-table-column prop="LevelOneOrgName" label="所属事业部" width="120" />
        <el-table-column prop="CourseName" label="课程名称" />
        <el-table-column prop="ProductTypeName" label="所属分类" />
        <el-table-column label="课程状态">
          <template slot-scope="scope">{{ scope.row.CourseStatus|formatCourseStatus }}</template>
        </el-table-column>
        <el-table-column label="提取状态">
          <template slot-scope="scope">{{ scope.row.ExtractStatus|formatExtractStatus }}</template>
        </el-table-column>
        <el-table-column label="课时单价">
          <template slot-scope="scope">{{ scope.row.FeeUnitPrice.toFixed(2) }}</template>
        </el-table-column>
        <el-table-column prop="FeeUnitPriceName" label="课时单位" />
        <el-table-column label="课时数">
          <template slot-scope="scope">{{ scope.row.TotalClassHour.toFixed(2) }}</template>
        </el-table-column>
        <el-table-column prop="CourseYear" label="所属年份" />
        <el-table-column prop="GradeName" label="所属年级" />
        <el-table-column prop="CategoryName" label="所属类型" />
        <el-table-column prop="SubjectName" label="所属科目" />
        <el-table-column prop="TermName" label="所属期段" />
        <el-table-column prop="ClassTypeName" label="所属班型" />
        <el-table-column prop="AuthorizeNum" label="授权校区(个)" width="120" />
        <el-table-column label="创建日期">
          <template slot-scope="scope">{{ scope.row.CreatedDate|formatDate('yyyy-MM-dd hh:mm:ss') }}</template>
        </el-table-column>
        <el-table-column label="更新日期">
          <template slot-scope="scope">{{ scope.row.UpdateDate|formatDate('yyyy-MM-dd hh:mm:ss') }}</template>
        </el-table-column>
      </el-table>
      <div class="paginationBox">
        <el-pagination
          :current-page="currentPage"
          :page-sizes="pagesizes"
          :page-size="pagesize"
          layout="total, sizes, prev, pager, next, jumper"
          :total="total"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
        />
      </div>

    </el-col>
  </div>
</template>

<script>
import SystemSource from '@/components/SystemSource'
import * as api from '@/api/ext_productcourse'
import * as Loading from '@/utils/loading'
export default {
  components: {
    SystemSource
  },
  filters: {
    formatCourseStatus(val) {
      let statusName = ''
      if (val === '0') {
        statusName = '无效'
      } else if (val === '1') {
        statusName = '有效'
      } else if (val === '-1') {
        statusName = '已删除'
      } else {
        statusName = '未知'
      }
      return statusName
    },
    formatExtractStatus(val) {
      let statusName = ''
      if (val === '1000') {
        statusName = '未提取'
      } else if (val === '2000') {
        statusName = '已提取'
      } else if (val === '3000') {
        statusName = '已关联'
      } else {
        statusName = '未知'
      }
      return statusName
    }
  },
  data() {
    return {
      formOptions: {
        pocSource: [], // 系统来源
        oneOrgId: [], // 所属事业部
        courseStatus: '', // 课程状态
        extractStatus: '', // 提取状态
        courseName: '' // 课程名称
      },
      tableData: [],
      currentPage: 1,
      pagesizes: [10, 20, 30, 40],
      pagesize: 10,
      total: 0
    }
  },
  created() {},
  methods: {
    getParameter() {
      const parameter = {
        PocSource: this.formOptions.pocSource,
        LevelOneOrgID: this.formOptions.oneOrgId,
        CourseStatus: this.formOptions.courseStatus,
        ExtractStatus: this.formOptions.extractStatus,
        CourseName: this.formOptions.courseName,
        PageIndex: this.currentPage,
        PageSize: this.pagesize
      }
      return parameter
    },
    async getTbale() {
      Loading.showLoading()
      const coursePageData = await api.GetProductCoursePageInfo(
        this.getParameter()
      )
      if (coursePageData.Code === '0') {
        this.tableData = coursePageData.Data.DataList
        this.total = coursePageData.Data.Total
      }
      Loading.hideLoading()
    },
    onSubmit() {
      if (this.formOptions.pocSource.length > 0) {
        this.currentPage = 1
        this.getTbale()
      } else {
        this.common.toast('系统来源必填', 'error')
      }
    },
    async onExport() {
      if (this.formOptions.pocSource.length > 0) {
        this.currentPage = 1
        const parameter = this.getParameter()
        parameter.IsExport = true
        const courseData = await api.exportExcle(parameter)
        var downloadElement = document.createElement('a')
        var href = window.URL.createObjectURL(courseData) // 创建下载的链接
        downloadElement.href = href
        downloadElement.download = '课程信息.xls' // 下载后文件名
        document.body.appendChild(downloadElement)
        downloadElement.click() // 点击下载
        document.body.removeChild(downloadElement) // 下载完成移除元素
        window.URL.revokeObjectURL(href) // 释放掉blob对象
      } else {
        this.common.toast('系统来源必填', 'error')
      }
    },
    handleSizeChange(val) {
      if (this.formOptions.pocSource.length <= 0) {
        this.common.toast('系统来源必填', 'error')
      } else {
        this.pagesize = val
        this.currentPage = 1
        this.getTbale()
      }
    },
    handleCurrentChange(val) {
      if (this.formOptions.pocSource.length <= 0) {
        this.common.toast('系统来源必填', 'error')
      } else {
        this.currentPage = val
        this.getTbale()
      }
    },
    getSystemSourceData(val) {
      this.formOptions.pocSource = val.pocSource
      this.formOptions.oneOrgId = val.oneOrgId
    }
  }
}
</script>

<style lang="scss" scoped>
.dashboard {
  &-container {
    margin: 30px;
  }
  &-text {
    font-size: 30px;
    line-height: 46px;
  }
}
.paginationBox{
  float: left;
  width: 100%;
}
.productCourseBox >>> .el-form-item__label{
  width: 100px;
  text-align: left;
}
@media only screen and (max-width: 1448px) {
  .marginTop {
    margin-top: 40px;
  }
}
</style>

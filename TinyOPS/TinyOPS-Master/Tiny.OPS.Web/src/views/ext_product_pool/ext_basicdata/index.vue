<!--事业部产品池-基础参数页面-->
<template>
  <div class="dashboard-container">
    <el-col :span="24">
      <el-form :inline="true" :model="formOptions" class="basicDataBox">
        <SystemSource @childFn="getSystemSourceData" />
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="参数值">
            <el-input v-model="formOptions.dictValue" />
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6 marginTop">
          <el-form-item>
            <el-button v-if="isExitPermissionPoint('EXT_BasicData_Query')" type="primary" @click="onSubmit">查询</el-button>
            <el-button v-if="isExitPermissionPoint('EXT_BasicData_Export')" type="primary" @click="onExport">导出</el-button>
          </el-form-item>
        </div>
      </el-form>
      <el-table :data="tableData" border style="width: 100%" empty-text=" ">
        <el-table-column prop="PocSourceName" label="系统来源" />
        <el-table-column prop="OneOrgName" label="所属事业部" />
        <el-table-column prop="DictTypeName" label="参数类型" />
        <el-table-column prop="DictValue" label="参数值" />
        <el-table-column prop="DictCreatetime" label="创建时间">
          <template slot-scope="scope">
            {{ scope.row.DictCreatetime|formatDate('yyyy-MM-dd hh:mm:ss') }}
          </template>
        </el-table-column>
        <el-table-column prop="DictUpdatetime" label="更新时间">
          <template slot-scope="scope">
            {{ scope.row.DictUpdatetime|formatDate('yyyy-MM-dd hh:mm:ss') }}
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        :current-page="currentPage"
        :page-sizes="pagesizes"
        :page-size="pagesize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="total"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
      />
    </el-col>
  </div>
</template>
<script>
import SystemSource from '@/components/SystemSource'
import * as api from '@/api/ext_basicdata'
import * as Loading from '@/utils/loading'
export default {
  components: {
    SystemSource
  },
  data() {
    return {
      formOptions: {
        pocSource: [], // 系统来源
        oneOrgId: [], // 所属事业部
        dictValue: '' // 参数值
      },
      tableData: [],
      currentPage: 1,
      pagesizes: [10, 20, 30, 40],
      pagesize: 10,
      total: 0
    }
  },
  created() {

  },
  methods: {
    getParameter() {
      const parameter = {
        PocSource: this.formOptions.pocSource,
        OneOrgId: this.formOptions.oneOrgId,
        DictValue: this.formOptions.dictValue,
        PageIndex: this.currentPage,
        PageSize: this.pagesize
      }
      return parameter
    },
    async getTbale() {
      Loading.showLoading()
      const coursePageData = await api.GetBasicDataInfoPage(this.getParameter())
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
        downloadElement.download = '基础数据.xls' // 下载后文件名
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
  .basicDataBox >>> .el-form-item__label{
    width: 100px;
    text-align: left;
  }
  @media only screen and (max-width: 1508px) {
    .basicDataBox >>> .el-form-item__label{
      width: 100%;
      text-align: left;
    }
    .marginTop {
      margin-top: 40px;
    }
  }
</style>

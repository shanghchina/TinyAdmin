<!--产品分类页面-->
<template>
  <div class="dashboard-container">
    <el-col :span="24">
      <el-form :inline="true" :model="formOptions" class="productitemtypeBox">
        <SystemSource @childFn="getSystemSourceData" />
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="产品分类状态">
            <el-select v-model="formOptions.State" placeholder="请选择" collapse-tags>
              <el-option label="全部" value="-1" />
              <el-option label="有效" value="1" />
              <el-option label="无效" value="0" />
            </el-select>
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="产品分类名称">
            <el-input v-model="formOptions.ProName" />
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item>
            <el-button v-if="isExitPermissionPoint('EXT_ProductItemType_Query')" type="primary" @click="onSearch">查询</el-button>
            <el-button v-if="isExitPermissionPoint('EXT_ProductItemType_Export')" type="primary" @click="onExport">导出</el-button>
          </el-form-item>
        </div>
      </el-form>
      <div>
        <el-table
          :data="tableData"
          border
          style="width: 100%"
          empty-text=" "
        >
          <el-table-column
            prop="PocSourceName"
            label="系统来源"
          />
          <el-table-column
            prop="TeachLevelOneOrgName"
            label="所属事业部"
          />
          <el-table-column
            prop="ProductTypeName"
            label="产品分类名称"
          />
          <el-table-column
            prop="NodeFlag"
            label="分类级数"
          />
          <el-table-column
            prop="ItemTypeStatus"
            label="产品分类状态"
          />
          <el-table-column prop="CreatedDate" label="创建时间">
            <template slot-scope="scope">
              {{ scope.row.CreatedDate|formatDate('yyyy-MM-dd hh:mm:ss') }}
            </template>
          </el-table-column>
          <el-table-column prop="UpdateDate" label="更新时间">
            <template slot-scope="scope">
              {{ scope.row.UpdateDate|formatDate('yyyy-MM-dd hh:mm:ss') }}
            </template>
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
      </div>
    </el-col>
  </div>
</template>

<script>
import SystemSource from '@/components/SystemSource'
import Api_ProductItemType from '@/api/ext_productitemtype'
import * as Loading from '@/utils/loading'

export default {
  components: {
    SystemSource
  },
  data() {
    return {
      formOptions: {
        FormSystem: [], // 系统来源
        OneOrgID: [], // 所属事业部
        State: '', // 产品分类状态
        ProName: '' // 产品分类名称
      },
      tableData: [
      ],
      currentPage: 1,
      pagesizes: [10, 20, 30, 40],
      pagesize: 10,
      total: 0
    }
  },
  created() {

  },
  methods: {
    onSearch() {
      if (this.formOptions.FormSystem.length > 0) {
        this.currentPage = 1
        this.getDataList()
      } else {
        this.common.toast('系统来源必填', 'error')
      }
    },
    async onExport() {
      if (this.formOptions.FormSystem.length > 0) {
        var request = {
          Source: this.formOptions.FormSystem,
          LevelOneOrgID: this.formOptions.OneOrgID,
          IsEffect: this.formOptions.State,
          ProductItemName: this.formOptions.ProName,
          PageIndex: this.currentPage,
          PageSize: this.pagesize
        }
        const courseData = await Api_ProductItemType.exportExcel(request)
        var downloadElement = document.createElement('a')
        var href = window.URL.createObjectURL(courseData) // 创建下载的链接
        downloadElement.href = href
        downloadElement.download = '产品分类.xls' // 下载后文件名
        document.body.appendChild(downloadElement)
        downloadElement.click() // 点击下载
        document.body.removeChild(downloadElement) // 下载完成移除元素
        window.URL.revokeObjectURL(href) // 释放掉blob对象
      } else {
        this.common.toast('系统来源必填', 'error')
      }
    },
    handleSizeChange(val) {
      if (this.formOptions.FormSystem.length > 0) {
        this.pagesize = val
        this.currentPage = 1
        this.getDataList()
      } else {
        this.common.toast('系统来源必填', 'error')
      }
    },
    handleCurrentChange(val) {
      if (this.formOptions.FormSystem.length > 0) {
        this.currentPage = val
        this.getDataList()
      } else {
        this.common.toast('系统来源必填', 'error')
      }
    }, getSystemSourceData(val) {
      this.formOptions.FormSystem = val.pocSource
      this.formOptions.OneOrgID = val.oneOrgId
    }, getDataList() {
      Loading.showLoading()
      var request = {
        Source: this.formOptions.FormSystem,
        LevelOneOrgID: this.formOptions.OneOrgID,
        IsEffect: this.formOptions.State,
        ProductItemName: this.formOptions.ProName,
        PageIndex: this.currentPage,
        PageSize: this.pagesize
      }
      Api_ProductItemType.getProductItemTypeList(request).then(response => {
        if (response.Code === '0') {
          this.tableData = response.Data.DataList
          this.total = response.Data.Total
        }
      })
      Loading.hideLoading()
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
@media only screen and (max-width: 1508px) {
  .productitemtypeBox >>> .el-form-item__label{
    width: 100%;
    text-align: left;
  }
  .marginTop {
    margin-top: 40px;
  }
}
.paginationBox{
  float: left;
  width: 100%;
}
</style>

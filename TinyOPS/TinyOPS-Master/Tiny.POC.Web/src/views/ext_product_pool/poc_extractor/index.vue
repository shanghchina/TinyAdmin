<!--产品提取器-->
<template>
  <div class="dashboard-container">
    <el-col :span="24">
      <el-form :inline="true" :model="formOptions" class="form_box">
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="系统来源">
            <el-select
              v-model="formOptions.formSystem"
              placeholder="请选择"
              multiple
              collapse-tags
              @change="fromSystemChange()"
            >
              <el-option
                v-for="item in formOptions.formSystemData"
                :key="item.SysDictKey"
                :value="item.DictGuid"
                :label="item.SysDictValue"
              />
            </el-select>
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="所属事业部">
            <el-select v-model="formOptions.oneOrgId" placeholder="请选择" multiple collapse-tags>
              <el-option
                v-for="item in formOptions.oneOrgIdData"
                :key="item.SysDictKey"
                :value="item.DictGuid"
                :label="item.SysDictValue"
              />
            </el-select>
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="提取器状态">
            <el-select v-model="formOptions.State" placeholder="请选择" collapse-tags>
              <el-option label="全部" value="0" />
              <el-option label="待提取" value="2100" />
              <!-- <el-option label="待确认" value="2200" /> -->
              <el-option label="已完成" value="2300" />
              <el-option label="已取消" value="2400" />
            </el-select>
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="基础参数">
            <el-cascader
              v-model="formOptions.selectedOptions"
              :options="formOptions.options"
              :props="formOptions.props"
              collapse-tags
              clearable
            />
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="提取器编号">
            <el-input v-model="formOptions.ExtractorId" />
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6 marginTop">
          <el-form-item>
            <el-button v-if="isExitPermissionPoint('POC_Extractor_Query')" type="primary" @click="onSearch">查询</el-button>
            <el-button v-if="isExitPermissionPoint('POC_Extractor_Export')" type="primary" @click="onExport">导出</el-button>
          </el-form-item>
        </div>
      </el-form>
      <div class="el-col el-col-sm-24 el-col-md-24 el-col-lg-24" style="margin: 20px 0">
        <el-button type="primary" @click="cancleExtractor()">取消提取器</el-button>
        <el-button v-if="isExitPermissionPoint('POC_Extractor_BatchAdd')" type="primary" @click="toggleAllSelection()">批量添加提取器</el-button>
      </div>
      <el-table
        ref="multipleTable"
        :data="extractorTable.extractorTableData"
        tooltip-effect="dark"
        style="width: 100%"
        border
        :row-key="getRowKeys"

        @selection-change="handleSelectionChange"
      >
        <el-table-column :reserve-selection="true" type="selection" width="55" />
        <el-table-column label="操作" width="120">
          <template slot-scope="scope">
            <el-button
              v-if="((scope.row.ExtractorStatus===2100 || scope.row.ExtractorStatus===2200)&& isExitPermissionPoint('POC_Extractor_BeginDo') )?true:false"
              type="primary"
              size="small"
              @click="handleExtractor(scope.row)"
            >处理</el-button>
          </template>
        </el-table-column>
        <el-table-column prop="ExtractorNo" label="提取器编号" width="120">
          <template slot-scope="scope">
            <el-button
              type="text"
              size="small"
              style="text-decoration: underline"
              @click="handleInfo(scope.row)"
            >{{ scope.row.ExtractorNo }}</el-button>
          </template>
        </el-table-column>
        <el-table-column prop="ExtractorStatusName" label="状态" />
        <el-table-column prop="ExtFromSystem" label="系统来源" />
        <el-table-column prop="OneOrg" label="所属事业部" show-overflow-tooltip width="150" />
        <el-table-column prop="ProductCount" label="涉及产品数量" width="150" />
        <el-table-column prop="ExtractCount" label="提取数量" width="100" />
        <el-table-column prop="AssociatedCount" label="关联数量" width="100" />
        <el-table-column prop="Year" label="所属年份" width="100" />
        <el-table-column prop="GradeName" label="所属年级" />
        <el-table-column prop="CategoryName" label="所属类型" how-overflow-tooltip width="100" />
        <el-table-column prop="SubjectName" label="所属科目" how-overflow-tooltip width="100" />
        <el-table-column prop="TermName" label="所属期段" how-overflow-tooltip width="100" />
        <el-table-column prop="ClassTypeName" label="所属班型" how-overflow-tooltip width="100" />
        <el-table-column prop="CreateDate" label="创建日期" width="200">
          <template slot-scope="scope">
            {{ scope.row.CreatedDate | formatDate('yyyy-MM-dd hh:mm:ss') }}
          </template>
        </el-table-column>
        <el-table-column prop="UpdateDate" label="更新日期" width="200">
          <template slot-scope="scope">
            {{ scope.row.UpdateDate | formatDate('yyyy-MM-dd hh:mm:ss') }}
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        :current-page="formOptions.pageIndex"
        :page-sizes="[10, 20, 30, 40]"
        :page-size="formOptions.pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="formOptions.totalCount"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
      />
    </el-col>
  </div>
</template>

<script>
import api from '@/api/poc_extractor'
import { showLoading, hideLoading } from '@/utils/loading'
import common from '@/utils/common'

export default {
  data() {
    return {
      getRowKeys(row) {
        return row.ExtractorDetailGuid
      },
      formOptions: {
        formSystem: [], // 系统来源
        formSystemData: [],
        oneOrgId: [], // 所属事业部
        oneOrgIdData: [], // 所属事业部
        State: '', // 产品分类状态
        extractorNo: '', // 提取器编号,
        props: { multiple: true },
        selectedOptions: [],
        pageIndex: 1,
        pageSize: 10,
        totalCount: 0,
        options: [
          {
            value: 1,
            label: '课程所属年级',
            children: [

            ]
          },
          {
            value: 2,
            label: '课程所属类型',
            children: [
            ]
          },
          {
            value: 3,
            label: '课程所属科目',
            children: [
            ]
          },
          {
            value: 4,
            label: '课程所属期段',
            children: [
            ]
          },
          {
            value: 5,
            label: '课程所属班型',
            children: [
            ]
          }
        ]
      },
      extractorTable: {
        extractorTableData: [],
        multipleSelection: []
      }
    }
  },
  created() {
    this.initFromSystem()
    this.initBaseInfo()
    this.getTable(false)
  },
  methods: {
    // 初始化基础参数
    async initBaseInfo() {
      showLoading()
      var that = this
      var request = { ParentGuid: ['2E9AED60-7EF3-477D-ABE3-0541F7DA3501', 'D7459004-5E26-43C7-B066-7F93F29D9A34', '1A930526-9F51-41BC-AC35-C890D0B60918', '0E0E674D-BBDF-4C3E-85AE-362E8FA5F3D5', 'AA98545B-495F-46FE-AA90-25C418E96C8C'] }
      var res = await api.getDict(request)
      if (res.Code === '0') {
        res.Data.dataList.forEach(function(item) {
          switch (item.ParentGuid.toUpperCase()) {
            // grade
            case '2E9AED60-7EF3-477D-ABE3-0541F7DA3501':
              that.formOptions.options[0].children.push({ value: item.DictGuid, label: item.SysDictValue })
              break
              // category
            case 'D7459004-5E26-43C7-B066-7F93F29D9A34':
              that.formOptions.options[1].children.push({ value: item.DictGuid, label: item.SysDictValue })
              break
              // subject
            case '1A930526-9F51-41BC-AC35-C890D0B60918':
              that.formOptions.options[2].children.push({ value: item.DictGuid, label: item.SysDictValue })
              break
              // term
            case '0E0E674D-BBDF-4C3E-85AE-362E8FA5F3D5':
              that.formOptions.options[3].children.push({ value: item.DictGuid, label: item.SysDictValue })
              break
              // classType
            case 'AA98545B-495F-46FE-AA90-25C418E96C8C':
              that.formOptions.options[4].children.push({ value: item.DictGuid, label: item.SysDictValue })
              break
          }
        })
      }
      hideLoading()
    },

    // 来源
    initFromSystem() {
      var request = { ParentGuid: ['C4AC5D00-0EDC-4B76-95AF-AC1EB1002A01'] }
      api.getDict(request).then(res => {
        if (res.Code === '0') {
          this.formOptions.formSystemData = res.Data.dataList
        }
      })
    },
    fromSystemChange() {
      this.initOneOrg()
    },
    // 事业部
    initOneOrg() {
      this.formOptions.oneOrgId = []
      if (this.formOptions.formSystem.length > 0) {
        var request = { ParentGuid: this.formOptions.formSystem }
        if (this.formOptions.formSystem) {
          api.getDict(request).then(res => {
            if (res.Code === '0') {
              this.formOptions.oneOrgIdData = res.Data.dataList
            }
          })
        } else {
          this.formOptions.oneOrgIdData = []
        }
      } else {
        this.formOptions.oneOrgIdData = []
      }
    },
    // 查询
    onSearch() {
      this.formOptions.pageIndex = 1
      this.getTable(true)
    },
    getTable(isCheck) {
      var request = {
        extFromSystem: this.formOptions.formSystem,
        levelOneGuid: this.formOptions.oneOrgId,
        extractorStatus: this.formOptions.State,
        extractorNo: this.formOptions.ExtractorId,
        // 年级
        gradeId: [],
        // 所属类型
        categoryId: [],
        // 所属科目
        subjectId: [],
        // 所属期望值
        termId: [],
        // 班型
        classTypeId: [],
        pageIndex: this.formOptions.pageIndex,
        pageSize: this.formOptions.pageSize
      }
      if (request.extFromSystem.length <= 0 && isCheck) {
        common.toast('请选择系统来源', 'error')
        return false
      }

      this.formOptions.selectedOptions.forEach(function(item) {
        switch (item[0]) {
          case 1:
            request.gradeId.push(item[1])
            break
          case 2:
            request.categoryId.push(item[1])
            break
          case 3:
            request.subjectId.push(item[1])
            break
          case 4:
            request.termId.push(item[1])
            break
          case 5:
            request.classTypeId.push(item[1])
            break
        }
      })
      showLoading()
      // 调用接口
      api.getExtractorList(request).then(res => {
        if (res.Code === '0') {
          this.extractorTable.extractorTableData = res.Data.dataList
          this.formOptions.totalCount = res.Data.totalCount
        } else {
          this.extractorTable.extractorTableData = []
        }
        hideLoading()
      })
    },
    onExport() {
      var request = {
        extFromSystem: this.formOptions.formSystem,
        levelOneGuid: this.formOptions.oneOrgId,
        extractorStatus: this.formOptions.State,
        extractorNo: this.formOptions.ExtractorId,
        // 年级
        gradeId: [],
        // 课程类型
        categoryId: [],
        // 所属科目
        subjectId: [],
        // 所属期望值
        termId: [],
        // 班型
        classTypeId: [],
        pageIndex: 1,
        pageSize: 10000
      }

      this.formOptions.selectedOptions.forEach(function(item) {
        switch (item[0]) {
          case 1:
            request.gradeId.push(item[1])
            break
          case 2:
            request.categoryId.push(item[1])
            break
          case 3:
            request.subjectId.push(item[1])
            break
          case 4:
            request.termId.push(item[1])
            break
          case 5:
            request.classTypeId.push(item[1])
            break
        }
      })
      if (request.extFromSystem.length <= 0) {
        common.toast('请选择系统来源', 'error')
        return false
      }
      // 调用接口
      showLoading()
      api.exportExcle(request).then(res => {
        hideLoading()
        var data = res
        var downloadElement = document.createElement('a')
        var href = window.URL.createObjectURL(data) // 创建下载的链接
        downloadElement.href = href
        downloadElement.download = '提取器.xls' // 下载后文件名
        document.body.appendChild(downloadElement)
        downloadElement.click() // 点击下载
        document.body.removeChild(downloadElement) // 下载完成移除元素
        window.URL.revokeObjectURL(href) // 释放掉blob对象
      })
    },
    handleInfo(row) {
      this.$router.push({ path: '/ext_product_pool/poc_extractor_detail', query: {
        id: row.ExtractorDetailGuid
      }})
    },
    handleExtractor(row) {
      this.$router.push({ path: '/ext_product_pool/poc_extractor_step01', query: {
        id: row.ExtractorDetailGuid
      }})
    },
    // 取消提取器
    cancleExtractor() {
      if (this.extractorTable.multipleSelection.length > 0) {
        if (this.extractorTable.multipleSelection.filter((x) => { return x.ExtractorStatus !== 2100 }).length > 0) {
          common.toast('只能取消待提取状态的提取器', 'error')
        } else {
          // 调用接口
          var request = { ExtractorGuids: this.extractorTable.multipleSelection.map(function(elem, index) { return elem.ExtractorDetailGuid }) }
          api.cancleExtractor(request).then(res => {
            if (res.Code === '0') {
              common.toast('取消成功', 'success')
              this.extractorTable.multipleSelection = []
              this.$refs.multipleTable.clearSelection()
              this.getTable(false)
            } else {
              common.toast('操作失败', 'error')
            }
          })
        }
      } else {
        common.toast('请选择提取器', 'error')
      }
    },
    handleSelectionChange(val) {
      this.extractorTable.multipleSelection = val
    },
    toggleAllSelection() {
      this.$router.push({ path: '/ext_product_pool/poc_extractor_muilt_add' })
    },
    handleSizeChange(val) {
      this.formOptions.pageSize = val
      this.formOptions.pageIndex = 1
      this.getTable(false)
    },
    handleCurrentChange(val) {
      this.formOptions.pageIndex = val
      this.getTable(false)
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
.form_box >>> .el-form-item__label{
  width: 100px;
  text-align: left;
}
@media only screen and (max-width: 1446px){
  .marginTop{
    margin-top: 40px;
  }
}
</style>

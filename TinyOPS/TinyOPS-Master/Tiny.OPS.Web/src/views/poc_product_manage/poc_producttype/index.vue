<!--产品分类管理页面-->
<template>
  <div class="dashboard-container">
    <el-button
      v-if="isExitPermissionPoint('POC_ProductType_Add')"
      size="medium"
      type="primary"
      style="margin-bottom: 20px"
      @click="handleClickAdd"
    >添加顶级分类         </el-button>
    <el-table
      :data="tableData"
      style="width: 100%;margin-bottom: 20px;"
      row-key="id"
      border
      :tree-props="{children: 'children', hasChildren: 'hasChildren'}"
      default-expand-all
    >
      <el-table-column
        prop="className"
        label="分类名称"
      />
      <el-table-column
        prop="level"
        label="级别"
      />
      <el-table-column
        prop="isStart"
        label="是否启用"
      >
        <template slot-scope="scope">
          <el-switch
            v-model="scope.row.isStart"
            style="display: block"
            active-color="#13ce66"
            inactive-color="#ff4949"
            @change="changeSwitch($event,scope.row)"
          />
        </template>
      </el-table-column>
      <el-table-column
        prop="remark"
        label="备注"
      />
      <el-table-column
        prop="productTypeGuid"
        label="映射关系"
      >
        <template slot-scope="scope">
          <el-button
            v-if="isExitPermissionPoint('POC_ProductType_View')"
            size="mini"
            type="primary"
            @click="handleRead(scope.$index, scope.row)"
          >查看</el-button>

        </template>
      </el-table-column>
      <el-table-column
        label="操作"
        width="300"
      >
        <template slot-scope="scope">

          <el-button
            v-if="isExitPermissionPoint('POC_ProductType_Add')&&scope.row.levelSort!==3"
            size="mini"
            type="primary"
            @click="handleAdd(scope.$index, scope.row)"
          >新增下级</el-button>
          <el-button
            v-if="isExitPermissionPoint('POC_ProductType_Edit')"
            size="mini"
            type="primary"
            @click="handleEdit(scope.$index, scope.row)"
          >编辑</el-button>
          <el-button
            v-if="isExitPermissionPoint('POC_ProductType_Delete')"
            size="mini"
            type="danger"
            @click="handleDelete(scope.$index, scope.row)"
          >删除</el-button>
        </template>
      </el-table-column>

    </el-table>
    <el-dialog title="映射关系" :visible.sync="dialogMapOption.dialogMapRalationsVisible">
      <p>当前产品分类：{{ showLevel }}</p>
      <el-table :data="dialogMapOption.gridData" border empty-text=" ">
        <el-table-column property="FromSystem" label="系统来源" />
        <el-table-column property="TeachLevelOneOrgName" label="所属事业部" />
        <el-table-column property="ProductTypeName" label="产品分类名称" />
        <el-table-column property="NodeFlag" label="分类级数" />
        <el-table-column property="MappingTime" label="映射时间" />
      </el-table>
      <el-pagination
        :current-page.sync="currentPage"
        :page-size="pagesize"
        :page-sizes="pagesizes"
        layout="total, sizes, prev, pager, next, jumper"
        :total="total"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
      />
      <div slot="footer" class="dialog-footer" style="text-align: center">
        <el-button type="primary" @click="dialogMapOption.dialogMapRalationsVisible = false">关闭</el-button>
      </div>
    </el-dialog>
    <el-dialog :title="dialogEditForm.title" :visible.sync="dialogEditForm.dialogFormVisible" width="30%">
      <el-form :model="dialogEditForm.form">

        <el-form-item label="分类名称" :label-width="dialogEditForm.formLabelWidth" prop="className">
          <el-input v-model="dialogEditForm.form.name" />
        </el-form-item>
        <el-form-item v-if="dialogEditForm.index!==1&&dialogEditForm.index!==-1" label="上级分类" :label-width="dialogEditForm.formLabelWidth">
          {{ dialogEditForm.form.parentName }}
          <p style="margin: 0;color: #999">请谨慎选择分类，添加后不可修改</p>
        </el-form-item>
        <el-form-item label="备注" :label-width="dialogEditForm.formLabelWidth">
          <el-input v-model="dialogEditForm.form.tipText" />
        </el-form-item>
        <el-form-item label="是否启用" :label-width="dialogEditForm.formLabelWidth">
          <el-switch v-model="dialogEditForm.form.state" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button type="primary" @click="operateProductType()">提交</el-button>
        <el-button @click="cancel">取 消</el-button>

      </div>
    </el-dialog>
  </div>
</template>

<script>
import Api_ProductType from '@/api/poc_producttype'
import * as Loading from '@/utils/loading'
export default {
  data() {
    return {
      userInfo: this.$store.getters.userInfo,
      tableData: [],
      dialogMapOption: {
        gridData: [],
        dialogMapRalationsVisible: false // 映射关系dialog
      },
      dialogEditForm: {
        dialogFormVisible: false,
        form: {
          name: '',
          tipText: '',
          parentName: '',
          state: true
        },
        index: -1, // table下标
        addOrUpdate: 0, // 0是添加，1是编辑
        title: '添加',
        row: [],
        formLabelWidth: '120px'
      },
      currentPage: 1,
      pagesizes: [10, 20, 30, 40],
      pagesize: 10,
      total: 0,
      showLevel: '',
      productTypeId: ''
    }
  },
  created() {
    this.getTableData()
  },
  methods: {
    cancel() {
      this.dialogEditForm.dialogFormVisible = false
      this.dialogEditForm.form.name = ''
      this.dialogEditForm.form.parentName = ''
      this.dialogEditForm.form.tipText = ''
      this.dialogEditForm.form.state = true
    },
    changeSwitch($event, row) {
      Loading.showLoading()
      Api_ProductType.operateSwitch({ ProductTypeGuid: row.productTypeGuid, IsEnabled: $event }).then(response => {
        if (response.Code === '0') {
          this.common.toast(response.Message, 'success')
          this.getTableData()
        }
      })
      Loading.hideLoading()
    },
    handleClickAdd() {
      this.dialogEditForm.dialogFormVisible = true
      this.dialogEditForm.index = -1
      this.dialogEditForm.addOrUpdate = 0
      this.dialogEditForm.form.name = ''
      this.dialogEditForm.form.parentName = ''
      this.dialogEditForm.form.tipText = ''
      this.dialogEditForm.form.state = true
      this.dialogEditForm.title = '添加'
    },
    handleRead(index, row) {
      this.showLevel = row.showLevel.concat().reverse().join('>')
      this.dialogMapOption.dialogMapRalationsVisible = true
      this.productTypeId = row.productTypeGuid
      this.mappingData(this.productTypeId)
    },
    handleAdd(index, row) {
      this.dialogEditForm.dialogFormVisible = true
      this.dialogEditForm.index = row.levelSort
      this.dialogEditForm.addOrUpdate = 0
      this.dialogEditForm.title = '添加'
      this.dialogEditForm.form.parentName = row.parentName
      this.dialogEditForm.form.name = ''
      this.dialogEditForm.form.tipText = ''
      this.dialogEditForm.form.state = true
      this.dialogEditForm.row = row
    },
    handleEdit(index, row) {
      this.dialogEditForm.dialogFormVisible = true
      this.dialogEditForm.index = row.levelSort
      this.dialogEditForm.addOrUpdate = 1
      this.dialogEditForm.title = '编辑'
      this.dialogEditForm.form.parentName = row.parentName
      this.dialogEditForm.form.name = row.className
      this.dialogEditForm.form.tipText = row.remark
      this.dialogEditForm.form.state = row.isStart
      this.dialogEditForm.row = row
    },
    handleDelete(index, row) {
      Loading.showLoading()
      Api_ProductType.deleteProductType({ ProductTypeGuid: row.productTypeGuid }).then(response => {
        if (response.Code === '0') {
          this.common.toast(response.Message)
          this.getTableData()
        }
      })
      Loading.hideLoading()
    },
    handleSizeChange(val) {
      this.pagesize = val
      this.currentPage = 1
      this.mappingData(this.productTypeId)
    },
    handleCurrentChange(val) {
      this.currentPage = val
      this.mappingData(this.productTypeId)
    },
    async getTableData() {
      Loading.showLoading()
      const data = await Api_ProductType.getProductTypeList({})
      if (data.Code === '0') {
        this.tableData = data.Data
      }
      Loading.hideLoading()
    },
    operateProductType() {
      if (this.dialogEditForm.form.name === '') {
        this.common.toast('分类名称不能为空', 'error')
        return false
      }
      var request = {
        Id: '',
        ProductTypeName: '',
        ProductTypeLevelNo: '',
        ProductTypeLevelName: '',
        ParentGuid: '',
        IsEnabled: '',
        Remark: '',
        UpdaterUserId: this.userInfo.UserGuid,
        UpdaterUserName: this.userInfo.Name
      }
      if (this.dialogEditForm.index === -1 && this.dialogEditForm.addOrUpdate === 0) { // 一级产品类别新增
        request.Id = -1
        request.ProductTypeName = this.dialogEditForm.form.name
        request.ProductTypeLevelNo = 1
        request.ProductTypeLevelName = '一级'
        request.ParentGuid = '00000000-0000-0000-0000-000000000000'
        request.IsEnabled = this.dialogEditForm.form.state
        request.Remark = this.dialogEditForm.form.tipText
      }

      if (this.dialogEditForm.index >= 1 && this.dialogEditForm.addOrUpdate === 0) { // 二三级产品类别新增
        request.Id = -1
        request.ProductTypeName = this.dialogEditForm.form.name
        if (this.dialogEditForm.row.levelSort === 1) {
          request.ProductTypeLevelNo = 2
          request.ProductTypeLevelName = '二级'
        }
        if (this.dialogEditForm.row.levelSort === 2) {
          request.ProductTypeLevelNo = 3
          request.ProductTypeLevelName = '三级'
        }
        request.ParentGuid = this.dialogEditForm.row.productTypeGuid
        request.IsEnabled = this.dialogEditForm.form.state
        request.Remark = this.dialogEditForm.form.tipText
      }
      if (this.dialogEditForm.index >= 1 && this.dialogEditForm.addOrUpdate === 1) { // 修改
        request.Id = this.dialogEditForm.row.id
        request.ProductTypeGuid = this.dialogEditForm.row.productTypeGuid
        request.ProductTypeName = this.dialogEditForm.form.name
        request.ProductTypeLevelNo = this.dialogEditForm.row.levelSort
        request.ProductTypeLevelName = this.dialogEditForm.row.level
        request.ParentGuid = this.dialogEditForm.row.parentGuid
        request.IsEnabled = this.dialogEditForm.form.state
        request.Remark = this.dialogEditForm.form.tipText
        request.CreatedDate = this.dialogEditForm.row.createdDate
        request.UpdaterUserId = this.userInfo.UserGuid
        request.UpdaterUserName = this.userInfo.Name
      }
      Loading.showLoading()
      Api_ProductType.operateProductType(request).then(response => {
        if (response.Code === '0') {
          this.getTableData()
          this.common.toast(response.Message, 'success')
        }
      })
      Loading.hideLoading()
      this.dialogEditForm.dialogFormVisible = false
      this.dialogEditForm.form.name = ''
      this.dialogEditForm.form.parentName = ''
      this.dialogEditForm.form.tipText = ''
      this.dialogEditForm.form.state = true
    },
    mappingData(productTypeGuid) {
      Loading.showLoading()
      Api_ProductType.getProductTypeMappingList({ ProductTypeId: productTypeGuid, PageIndex: this.currentPage, PageSize: this.pagesize }).then(response => {
        if (response.Code === '0') {
          this.dialogMapOption.gridData = response.Data.DataList
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
</style>

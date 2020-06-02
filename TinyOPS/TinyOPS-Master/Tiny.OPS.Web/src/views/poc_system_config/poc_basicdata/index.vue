<!--系统设置-基础参数设置-->
<template>
  <div class="dashboard-container">
    <div class="el-col el-col-sm-24 el-col-md-24 el-col-lg-24 poc_content_tit">基础参数设置</div>
    <div class="table_box">
      <el-table :data="pocParamTableData" border style="width: 100%" empty-text=" ">
        <el-table-column
          prop="SysCatalogName"
          label="参数类型"
        />
        <el-table-column
          prop="SysDictDesc"
          label="参数描述"
        />
        <el-table-column
          prop="ChinldSysDictValueLable"
          label="可选值列表"
        />
        <el-table-column
          prop="SysDictSort"
          label="排序"
        />
        <el-table-column
          label="操作"
        >
          <template slot-scope="scope">
            <el-button v-if="isExitPermissionPoint('POC_BasicData_Edit')" type="primary" size="small" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
          </template>
        </el-table-column>
      </el-table>
    </div>
    <el-dialog title="编辑参数" :visible.sync="editDialogFormVisible" width="40%">
      <el-form ref="editDialogValidateForm" :rules="editDialogFormRule" :model="formData" class="demo-ruleForm">

        <el-form-item label="参数名称" prop="SysDictValue" :label-width="formData.formLabelWidth">
          <el-input v-model="formData.SysDictValue" maxlength="20" />
        </el-form-item>
        <el-form-item label="参数描述" prop="SysDictDesc" :label-width="formData.formLabelWidth">
          <el-input v-model="formData.SysDictDesc" maxlength="50" />
        </el-form-item>

        <div class="content_bd">
          <div class="label_left">参数选项</div>
          <div class="content_right" >
            <el-button type="primary" size="small" @click="handleSelectOptionAdd" style="margin-bottom: 20px">新增选项</el-button>
            <el-table
              :data="formData.selectOptions"
              style="width: 100%"
              border
            >
              <el-table-column prop="SysDictValue" label="选项名称" :render-header="showMark">
                <template slot-scope="scope">
                  <el-form-item :prop="'selectOptions.' + scope.$index + '.SysDictValue'" :rules="formData.rules.SysDictValue">
                    <el-input v-model="scope.row.SysDictValue" maxlength="20"  />
                  </el-form-item>
                </template>
              </el-table-column>
              <el-table-column prop="SysDictDesc" label="选项描述">
                <template slot-scope="scope">
                  <el-form-item :prop="'selectOptions.' + scope.$index + '.SysDictDesc'" :rules="formData.rules.SysDictDesc">
                    <el-input v-model="scope.row.SysDictDesc" maxlength="50" />
                  </el-form-item>
                </template>
              </el-table-column>

              <el-table-column label="操作">
                <template slot-scope="scope">
                  <el-button type="primary"  size="small" @click="handleSelectOptionDelete(scope.$index, scope.row)">删除</el-button>
                </template>
              </el-table-column>
            </el-table>
          </div>
        </div>
        <el-form-item label="排序" prop="SysDictSort" :label-width="formData.formLabelWidth">
          <el-input v-model="formData.SysDictSort" @keyup.native="proving" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button type="primary" @click="handleSumbit('editDialogValidateForm')">提交</el-button>
        <el-button @click="editDialogFormVisible = false">取 消</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import * as api from '../../../api/poc_basicdata'
import * as Loading from '@/utils/loading'
export default {
  data() {
    return {
      requsetWhereData: {
        ParentGuid: '00000000-0000-0000-0000-000000000000'
      },
      // 产品中心-基础参数大类列表
      pocParamTableData: [{}],
      editDialogFormVisible: false,
      // 基础参数子节点列表
      editDialogFormData: {
        editDialogForm: {
          SysDictValue: '',
          SysDictDesc: '',
          SysDictSort: ''
        },
        formLabelWidth: '80px',
        selectOptions: [{
          sysDictValue: '',
          SysDictDesc: ''
        }, {
          sysDictValue: '',
          SysDictDesc: ''
        }]
      },
      editDialogFormRule: {
        SysDictValue: [
          { required: true, message: '请输入参数名称', trigger: 'blur' },
          { min: 1, max: 20, message: '长度不能超过20个字符', trigger: 'blur' }
        ],
        SysDictSort: [{ required: true, message: '必填', trigger: 'blur' }],
        SysDictDesc: [{ min: 1, max: 50, message: '长度不能超过50个字符', trigger: 'blur' }]
      },
      formData: {
        rules: {
          SysDictValue: [
            { type: 'string', required: true, message: '请输入参数名称', trigger: 'blur' },
            { min: 1, max: 20, message: '长度不能超过20个字符', trigger: 'blur' }
          ],
          SysDictDesc: [
            {
              min: 1,
              max: 50,
              message: '长度不能超过50个字符',
              trigger: 'blur'
            }
          ]
        },
        selectOptions: [
          {
            SysDictValue: '',
            SysDictDesc: ''
          },
          {
            SysDictValue: '',
            SysDictDesc: ''
          }
        ],
        SysDictValue: '',
        SysDictDesc: '',
        SysDictSort: '',
        formLabelWidth: '120px',
        parentGuid: '00000000-0000-0000-0000-000000000000',
        vmParentDict: {}// 父节点数据
      }
    }
  },
  created() {
    console.log('onDoQuery!')
    this.getPOCBasiceParamsList()
    console.log('onDoQuery successed!')
  },
  methods: {
    onDoQuery() {
      console.log('onDoQuery!')
      this.getPOCBasiceParamsList()
      console.log('onDoQuery successed!')
    },
    proving() {
      this.formData.SysDictSort = this.formData.SysDictSort.replace(/[^\.\d]/g, '')
      this.formData.SysDictSort = this.formData.SysDictSort.replace('.', '')
      if ((this.formData.SysDictSort) > 999999999) {
        this.formData.SysDictSort = 999999999
      }
    },
    // 编辑
    handleEdit(index, row) {
      this.editDialogFormVisible = true
      console.log(index, row)
      console.log('正在获取子节点')
      this.getPOCBasiceChildParamsList(row)
    },
    // 删除行
    handleSelectOptionDelete(index, row) {
      console.log(index, row)
      // debugger
      this.checkHasMappedBasciData(index, row.DictGuid)
      console.log('handleSelectOptionDelete end')
    },
    // 新增行
    handleSelectOptionAdd() {
      const newValue = {
        selectOptions: '',
        sysDictValue: ''
      }
      this.formData.selectOptions.push(newValue)
    },
    // 提交
    handleSumbit(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          // debugger
          console.log(this.formData.selectOptions)
          console.log(formName)
          console.log(this.$refs[formName])
          this.submitChildParamsOption()
          console.log(' handleSumbit end')
        } else {
          console.log('error handleSumbit!!')
          return false
        }
      })
      // this.editDialogFormVisible=false
    },
    showMark(h, { column, $index }) { // 此法直观插入html片段变量column.label要一个花括号括起来
      return (<span><font style='color:red;'>*</font>&nbsp;<label>{column.label}</label></span>)
    },
    // 系统维护-基础参数大类列表
    async getPOCBasiceParamsList() {
      console.log('begin do getPOCBasiceParamsList......')
      Loading.showLoading()
      try {
        const data = await api.getPOCBasiceParamsList()
        // debugger
        if (
          data.Code.toString() === '0'
        ) {
          console.log('getPOCBasiceParamsList success')
          // debugger
          this.pocParamTableData = data.Data.ReusltList
        } else {
          console.log('无数据')
          this.common.toast('无数据')
        }
      } catch (e) {
        // this.$indicator.close()
        console.log(e)
      }
      Loading.hideLoading()
    },
    // 系统维护-基础参数子节点
    async getPOCBasiceChildParamsList(row) {
      console.log('begin do getPOCBasiceChildParamsList......')
      Loading.showLoading()
      try {
        console.log(row)
        this.formData.SysDictValue = row.SysDictValue
        this.formData.SysDictDesc = row.SysDictDesc
        this.formData.SysDictSort = row.SysDictSort

        const strParentGuid = row.DictGuid
        // debugger
        this.requsetWhereData.ParentGuid = strParentGuid
        this.formData.vmParentDict = row
        console.log(this.requsetWhereData)
        const data = await api.getPOCBasiceChildParamsList(this.requsetWhereData)
        // debugger
        if (
          data.Code.toString() === '0'
        ) {
          console.log('getPOCBasiceChildParamsList success')
          // debugger
          console.log(data.Data)
          this.formData.selectOptions = data.Data.ReusltList
          console.log(this.formData.selectOptions)
        } else {
          console.log('无数据')
          this.common.toast('无数据')
        }
      } catch (e) {
        // this.$indicator.close()
        console.log(e)
      }
      Loading.hideLoading()
    },
    // 系统维护-基础参数子节点-提交保存
    async submitChildParamsOption() {
      console.log('begin do submitChildParamsOption......')
      try {
        this.formData.parentGuid = '00000000-0000-0000-0000-000000000000'
        this.formData.parentGuid = this.requsetWhereData.ParentGuid
        console.log(this.formData.selectOptions)
        debugger
        const data = await api.submitChildParamsOption(this.formData)
        // debugger
        if (
          data.Code.toString() === '0'
        ) {
          debugger
          console.log('submitChildParamsOption success')
          console.log(data.Data)
          this.formData.selectOptions = data.Data.ReusltList
          console.log(this.formData.selectOptions)

          this.getPOCBasiceParamsList()
          this.editDialogFormVisible = false
        } else {
          debugger
          console.log(data.Message)
          this.common.toast(data.Message, 'error')
        }
      } catch (e) {
        console.log(e)
      }
    },
    // 系统维护-基础参数子节点-删除子节点判断是否已经映射
    async checkHasMappedBasciData(index, dictGuid) {
      console.log('begin do checkHasMappedBasciData......')
      try {
        const jsonObj = { 'dictGuid': dictGuid }
        console.log(jsonObj)
        // 校验基础参数映射
        const data = await api.checkHasMappedBasciData(jsonObj)
        if (data.Code.toString() === '-1') {
          debugger
          console.log('checkHasMappedBasciData success')
          console.log(data)
          if (data.Data) {
            console.log('存在映射')
            this.common.toast(data.Message, 'error')
          }
        } else {
          console.log('无数据，删除节点')
          this.formData.selectOptions.splice(index, 1)
        }
      } catch (e) {
        console.log(e)
      }
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
  .poc_content_tit{
    border-left: 3px solid #409eff;
    padding-left: 10px;
    color: #409eff;
    font-size: 16px;
    margin-bottom: 20px;
  }
  .content_bd{
    /*display: table;*/
    margin-bottom: 20px;
  }
  .content_bd .label_left{
    text-align: right;
    vertical-align: middle;
    float: left;
    font-size: 14px;
    color: #606266;
    line-height: 40px;
    padding: 0 12px 0 0;
    -webkit-box-sizing: border-box;
    box-sizing: border-box;
    width: 120px;
    font-weight: 700;
  }
  .content_bd .content_right{
    /*line-height: 40px;*/
    position: relative;
    font-size: 14px;
    margin-left: 120px;
    /*width: 100%;*/
  }
  .content_right .el-input{
    margin-top: 25px;
  }
  .content_right >>> .el-table tbody td,.content_right >>> .el-table tbody th{
    padding: 0;
  }
</style>

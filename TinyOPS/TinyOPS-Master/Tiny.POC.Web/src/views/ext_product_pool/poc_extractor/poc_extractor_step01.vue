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
      <el-col :span="24">
        <el-form :inline="true" :model="formOptions" class="demo-form-inline">
          <div class="el-col el-col-sm-12 el-col-md-12 el-col-lg-12 current_label_w">
            <el-form-item label="系统来源">
              {{ (formOptions.fromSystem.substring(formOptions.fromSystem.length-1)=='，')?formOptions.fromSystem.substring(0,formOptions.fromSystem.length-1):formOptions.fromSystem }}
            </el-form-item>
          </div>
          <div class="el-col el-col-sm-12 el-col-md-12 el-col-lg-12 current_label_w">
            <el-form-item label="所属事业部">
              {{ (formOptions.oneOrg.substring(formOptions.oneOrg.length-1)=='，')?formOptions.oneOrg.substring(0,formOptions.oneOrg.length-1):formOptions.oneOrg }}
            </el-form-item>
          </div>
          <div class="el-col el-col-sm-12 el-col-md-12 el-col-lg-12 current_label_w">
            <el-form-item label="产品状态">
              {{ formOptions.proStatueName }}
            </el-form-item>
          </div>
          <div class="el-col el-col-sm-12 el-col-md-12 el-col-lg-12 current_label_w">
            <el-form-item label="产品动销阈值">
              {{ formOptions.thresholdName }}
            </el-form-item>
          </div>
          <div class="selectOptions">
            <div class="selectOptions_l ">基础参数</div>
            <div class="selectOptions_r">
              <div class="select_item_box">
                <div class="items_list">
                  <div class="label_box">
                    课程所属年份
                  </div>
                  <div class="select_checkbox_box">
                    <el-checkbox-group v-model="formOptions.year">
                      <el-checkbox v-for="(item,index) in formOptions.yearData" :key="index" :disabled="true" :label="item">{{ item }}</el-checkbox>
                    </el-checkbox-group>
                  </div>
                </div>
                <div class="items_list">
                  <div class="label_box">
                    课程所属年级
                  </div>
                  <div class="select_checkbox_box">
                    <el-checkbox-group v-model="formOptions.gradeId">
                      <el-checkbox v-for="(item,index) in formOptions.gradeIdData" :key="index" :disabled="true" :label="item.DictGuid">{{ item.SysDictValue }}</el-checkbox>
                    </el-checkbox-group>
                  </div>
                </div>
                <div class="items_list">
                  <div class="label_box">
                    课程所属类型
                  </div>
                  <div class="select_checkbox_box">
                    <el-checkbox-group v-model="formOptions.categoryId">
                      <el-checkbox v-for="(item,index) in formOptions.categoryIdData" :key="index" :disabled="true" :label="item.DictGuid">{{ item.SysDictValue }}</el-checkbox>
                    </el-checkbox-group>
                  </div>
                </div>
                <div class="items_list">
                  <div class="label_box">
                    课程所属科目
                  </div>
                  <div class="select_checkbox_box">
                    <el-checkbox-group v-model="formOptions.subjectId">
                      <el-checkbox v-for="(item,index) in formOptions.subjectIdData" :key="index" :disabled="true" :label="item.DictGuid">{{ item.SysDictValue }}</el-checkbox>
                    </el-checkbox-group>
                  </div>
                </div>
                <div class="items_list">
                  <div class="label_box">
                    课程所属期段
                  </div>
                  <div class="select_checkbox_box">
                    <el-checkbox-group v-model="formOptions.termId">
                      <el-checkbox v-for="(item,index) in formOptions.termIdData" :key="index" :disabled="true" :label="item.DictGuid">{{ item.SysDictValue }}</el-checkbox>
                    </el-checkbox-group>
                  </div>
                </div>
                <div class="items_list">
                  <div class="label_box">
                    课程所属班型
                  </div>
                  <div class="select_checkbox_box">
                    <el-checkbox-group v-model="formOptions.classTypeId">
                      <el-checkbox v-for="(item,index) in formOptions.classTypeIdData" :key="index" :disabled="true" :label="item.DictGuid">{{ item.SysDictValue }}</el-checkbox>
                    </el-checkbox-group>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="el-col el-col-sm-24 el-col-md-24 el-col-lg-24" style="text-align: center;margin-top: 20px;margin-bottom: 100px">
            <el-form-item>
              <el-button type="primary" size="medium" @click="nextStep">下一步</el-button>
              <el-button size="medium" @click="cancle">取消</el-button>
            </el-form-item>
          </div>
        </el-form>

      </el-col>
    </div>
  </div>
</template>
<script>
import { showLoading, hideLoading } from '@/utils/loading'
import api from '@/api/poc_extractor'
export default {
  data() {
    return {
      active: 1,
      formOptions: {
        fromSystem: '', // 来源
        fromSystemValue: [],
        oneOrg: '', // 所属事业部
        oneOrgValue: [],
        proStatueName: '', // 产品状态
        proStatue: '', // 产品状态
        thresholdName: '', // 产品动销阈值
        thresholdValue: '',
        year: [], // 所属年份
        yearData: [(new Date().getFullYear() - 3).toString(), (new Date().getFullYear() - 2).toString(), (new Date().getFullYear() - 1).toString(), (new Date().getFullYear()).toString(), (new Date().getFullYear() + 1).toString()], // 年份数据
        gradeId: [], // 所属年级
        gradeIdData: [], // 所属年级
        categoryId: [], // 课程类型
        categoryIdData: [], // 课程类型
        subjectId: [], // 所属科目
        subjectIdData: [], // 所属科目
        termId: [], // 所属期望值
        termIdData: [], // 所属期望值
        classTypeId: [], // 所属班型
        classTypeIdData: [] // 所属班型
      },
      preUrl: ''
    }
  },
  mounted() {
    this.initBaseInfo()
  },
  beforeRouteEnter(to, from, next) {
    next(vm => {
      // 通过 `vm` 访问组件实例,将值传入oldUrl
      vm.preUrl = from.path
    })
  },
  methods: {
    async initBaseInfo() {
      var that = this
      showLoading()
      var request = { ParentGuid: ['2E9AED60-7EF3-477D-ABE3-0541F7DA3501', 'D7459004-5E26-43C7-B066-7F93F29D9A34', '1A930526-9F51-41BC-AC35-C890D0B60918', '0E0E674D-BBDF-4C3E-85AE-362E8FA5F3D5', 'AA98545B-495F-46FE-AA90-25C418E96C8C'] }
      var res = await api.getDict(request)
      if (res.Code === '0') {
        res.Data.dataList.forEach(function(item) {
          switch (item.ParentGuid.toUpperCase()) {
            // grade
            case '2E9AED60-7EF3-477D-ABE3-0541F7DA3501':
              that.formOptions.gradeIdData.push(item)
              break
              // category
            case 'D7459004-5E26-43C7-B066-7F93F29D9A34':
              that.formOptions.categoryIdData.push(item)
              break
              // subject
            case '1A930526-9F51-41BC-AC35-C890D0B60918':
              that.formOptions.subjectIdData.push(item)
              break
              // term
            case '0E0E674D-BBDF-4C3E-85AE-362E8FA5F3D5':
              that.formOptions.termIdData.push(item)
              break
              // classType
            case 'AA98545B-495F-46FE-AA90-25C418E96C8C':
              that.formOptions.classTypeIdData.push(item)
              break
          }
        })
      }
      // 获取提取器信息
      var request1 = { extractorDetailGuid: this.$route.query.id }
      var res1 = await api.getExtractorInfo(request1)
      if (res1.Code === '0') {
        this.formOptions.year.push(res1.Data.extractorDetail.Year + '')
        this.formOptions.gradeId.push(res1.Data.extractorDetail.GradeID.toLowerCase())
        this.formOptions.categoryId.push(res1.Data.extractorDetail.CategoryID.toLowerCase())
        this.formOptions.subjectId.push(res1.Data.extractorDetail.SubjectID.toLowerCase())
        this.formOptions.termId.push(res1.Data.extractorDetail.TermID.toLowerCase())
        this.formOptions.classTypeId.push(res1.Data.extractorDetail.ClassTypeID.toLowerCase())
        res1.Data.queryList.forEach((item) => {
          switch (item.SelectFieldTypeCode) {
            case 'ExtFromSystem':
              this.formOptions.fromSystem += item.SelectFieldTypeName + '，'
              this.formOptions.fromSystemValue.push(item.SelectFieldTypeId)
              break
            case 'OneOrg':
              this.formOptions.oneOrg += item.SelectFieldTypeName + '，'
              this.formOptions.oneOrgValue.push(item.SelectFieldTypeId)
              break
            case 'ProductStatus':
              this.formOptions.proStatueName += item.SelectFieldTypeName
              this.formOptions.proStatue = item.SelectFieldTypeId
              break
            case 'ThresholdValue':
              this.formOptions.thresholdName += item.SelectFieldTypeName
              this.formOptions.thresholdValue = item.SelectFieldTypeId
              break
          }
        })
      }
      // 将提取器信息存储缓存中
      var condition = {}
      var request4 = { DictGuid: this.formOptions.fromSystemValue.concat(this.formOptions.oneOrgValue) }
      var res4 = await api.getDict(request4)

      if (res4.Code === '0') {
        condition.fromSystems = res4.Data.dataList.filter((ele) => { return that.formOptions.fromSystemValue.indexOf(ele.DictGuid) >= 0 }).map(function(elem, index) { return elem.SysDictKey })
        condition.oneOrgIds = res4.Data.dataList.filter((ele) => { return that.formOptions.oneOrgValue.indexOf(ele.DictGuid) >= 0 }).map(function(elem, index) { return elem.SysDictKey })
      }
      condition.courseStatus = this.formOptions.proStatue
      condition.thresholdValue = this.formOptions.thresholdValue
      condition.courseYear = this.formOptions.year[0]
      condition.gradeId = []
      condition.categoryId = []
      condition.subjectId = []
      condition.termId = []
      condition.classTypeId = []
      condition.pageindex = 1
      condition.pageSize = 10
      condition.isCount = false
      // 获取所有映射参数
      var reqeust2 = { pocSource: condition.fromSystems, fkDictGuid: that.formOptions.gradeId.concat(that.formOptions.categoryId).concat(that.formOptions.termId).concat(that.formOptions.subjectId).concat(that.formOptions.classTypeId) }
      var res3 = await api.getBaseData(reqeust2)
      if (res3.Code === '0') {
        res3.Data.forEach((item) => {
          switch (item.FKDictGuid) {
            case that.formOptions.gradeId[0]:
              condition.gradeId.push(item.DictId)
              break
            case that.formOptions.categoryId[0]:
              condition.categoryId.push(item.DictId)
              break
            case that.formOptions.termId[0]:
              condition.termId.push(item.DictId)
              break
            case that.formOptions.subjectId[0]:
              condition.subjectId.push(item.DictId)
              break
            case that.formOptions.classTypeId[0]:
              condition.classTypeId.push(item.DictId)
              break
          }
        })
      }
      if (condition.gradeId.length <= 0) {
        condition.gradeId.push('99999999-9999-9999-9999-999999999999')
      }
      if (condition.categoryId.length <= 0) {
        condition.categoryId.push('99999999-9999-9999-9999-999999999999')
      }
      if (condition.subjectId.length <= 0) {
        condition.subjectId.push('99999999-9999-9999-9999-999999999999')
      }
      if (condition.termId.length <= 0) {
        condition.termId.push('99999999-9999-9999-9999-999999999999')
      }
      if (condition.classTypeId.length <= 0) {
        condition.classTypeId.push('99999999-9999-9999-9999-999999999999')
      }
      // 提取器产品的参数
      var extractorCondition = {}
      extractorCondition.GradeDictGuid = that.formOptions.gradeId[0]
      if (that.formOptions.gradeIdData.filter((item) => { return item.DictGuid === that.formOptions.gradeId[0] }).length > 0) {
        extractorCondition.GradeDictName = that.formOptions.gradeIdData.filter((item) => { return item.DictGuid === that.formOptions.gradeId[0] })[0].SysDictValue
      } else {
        extractorCondition.GradeDictName = ''
      }
      extractorCondition.CategoryDictGuid = that.formOptions.categoryId[0]
      if (that.formOptions.categoryIdData.filter((item) => { return item.DictGuid === that.formOptions.categoryId[0] }).length > 0) {
        extractorCondition.CategoryDictName = that.formOptions.categoryIdData.filter((item) => { return item.DictGuid === that.formOptions.categoryId[0] })[0].SysDictValue
      } else {
        extractorCondition.CategoryDictName = ''
      }
      extractorCondition.SubjectDictGuid = that.formOptions.subjectId[0]
      if (that.formOptions.subjectIdData.filter((item) => { return item.DictGuid === that.formOptions.subjectId[0] }).length > 0) {
        extractorCondition.SubjectDictName = that.formOptions.subjectIdData.filter((item) => { return item.DictGuid === that.formOptions.subjectId[0] })[0].SysDictValue
      } else {
        extractorCondition.SubjectDictName = ''
      }
      extractorCondition.TermDictGuid = that.formOptions.termId[0]
      if (that.formOptions.termIdData.filter((item) => { return item.DictGuid === that.formOptions.termId[0] }).length > 0) {
        extractorCondition.TermDictName = that.formOptions.termIdData.filter((item) => { return item.DictGuid === that.formOptions.termId[0] })[0].SysDictValue
      } else {
        extractorCondition.TermDictName = ''
      }
      extractorCondition.ClassTypeDictGuid = that.formOptions.classTypeId[0]
      if (that.formOptions.classTypeIdData.filter((item) => { return item.DictGuid === that.formOptions.classTypeId[0] }).length > 0) {
        extractorCondition.ClassTypeDictName = that.formOptions.classTypeIdData.filter((item) => { return item.DictGuid === that.formOptions.classTypeId[0] })[0].SysDictValue
      } else {
        extractorCondition.ClassTypeDictName = ''
      }
      // 查询产品池产品的参数
      sessionStorage.setItem('condition', JSON.stringify(condition))
      sessionStorage.setItem('extractorCondition', JSON.stringify(extractorCondition))
      hideLoading()
    },
    nextStep() {
      if (this.preUrl === '/ext_product_pool/poc_extractor') {
        sessionStorage.removeItem('products')
        sessionStorage.removeItem('relationArr')
      }

      this.$router.push({ path: '/ext_product_pool/poc_extractor_step02', query: {
        id: this.$route.query.id
      }})
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
  }

  .poc_add_container .step_panel{
   width: 100%;
    clear: both;
    margin-top: 60px;
    margin-bottom: 100px;
    float: left;
  }
  .label_items_box ul{

  }
  .label_items_box ul li{
    width: 50%;
    float: left;
    list-style: none;
    height: 40px;
    line-height: 40px;
    font-size: 14px;
    color: #606266;
  }
  .label_items_box ul li span{
    width: 100px;
    text-align: left;
    display: inline-block;
  }
  .poc_add_container{
    width: 70%;
    margin: 0 auto;
  }
  .selectOptions_l{
    width: 100px;
    float: left;
    color: #606266;
    font-size: 14px;
    font-weight: 700;

  }
  .selectOptions_r{
    width: 87%;
    float: left;
  }
  .selectOptions_r .select_item_box{
    padding:30px 0 20px 30px;
    width: 100%;
    float: left;
    background: #fafafa;
    border: 1px solid #eaeefb;
    margin-bottom: 20px;
  }
  .selectOptions_r .label_box{
    width: 100px;
    float: left;
    font-size: 14px;
    color: #606266;
  }
  .selectOptions_r .select_checkbox_box{
    width: 83%;
    float: left;
    padding-left: 20px;

  }
  .items_list{
    width: 100%;
    float: left;
    line-height: 34px;
    margin-bottom:10px;
  }
  .dialog_box_tip{
    width: 100%;
    clear: both;
    margin-top: 20px;
    color: red;
  }
  .dialog_box_tip span{
    text-decoration: underline;
    cursor: pointer;
  }
  .p_title{
    padding-left: 5px;
    margin-top: 10px;
    color: red;
    margin-bottom: 10px;
  }
  .current_label_w  >>>.el-form-item__label{
    width: 100px;
    text-align: left;
  }
 .items_list  >>> .is-checked  .el-checkbox__inner{background-color: #409EFF !important;border-color: #409EFF !important;}
 .items_list  >>> .is-checked  .el-checkbox__label{color: #409EFF !important;}
</style>

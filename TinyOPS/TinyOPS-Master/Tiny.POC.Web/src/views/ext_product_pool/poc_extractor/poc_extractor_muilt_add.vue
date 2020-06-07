<!--批量添加提取器-->
<template>
  <div class="dashboard-container">
    <div class="el-col el-col-sm-24 el-col-md-24 el-col-lg-24 poc_content_tit">批量添加提取器</div>
    <div class="poc_add_container">
      <p v-if="isShowMessage" class="p_text_tip">系统提示： 事业部产品池的<span v-if="isBaseDataMapping===false">基础参数，</span><span v-if="isProductTypeMapping===false">产品分类</span>存在未映射数据，请先进行映射之后再添加提取器</p>
      <el-col :span="24">
        <el-form :inline="true" :model="formOptions" class="muiltAddBox">
          <div class="el-col el-col-sm-12 el-col-md-12 el-col-lg-12 current_label_w">
            <el-form-item label="系统来源">
              <el-select
                v-model="formOptions.fromSystem"
                placeholder="请选择"
                multiple
                collapse-tags
                @change="fromSystemChange()"
              >
                <el-option
                  v-for="(item,index) in formOptions.fromSystemData"
                  :key="index"
                  :value="item.DictGuid"
                  :label="item.SysDictValue"
                />
              </el-select>
            </el-form-item>
          </div>
          <div class="el-col el-col-sm-12 el-col-md-12 el-col-lg-12 current_label_w">
            <el-form-item label="所属事业部">
              <el-select
                v-model="formOptions.oneOrgId"
                placeholder="请选择"
                multiple
                collapse-tags
                @change="createData()"
              >
                <el-option
                  v-for="(item,index) in formOptions.oneOrgIdData"
                  :key="index"
                  :value="item.DictGuid"
                  :label="item.SysDictValue"
                />
              </el-select>
            </el-form-item>
          </div>
          <div class="el-col el-col-sm-12 el-col-md-12 el-col-lg-12 current_label_w">
            <el-form-item label="产品状态">
              <el-select v-model="formOptions.proStatue" placeholder="请选择" @change="createData()">
                <el-option
                  v-for="(item,index) in formOptions.proStatueData"
                  :key="index"
                  :value="item.value"
                  :label="item.name"
                />
              </el-select>
            </el-form-item>
          </div>
          <div class="el-col el-col-sm-12 el-col-md-12 el-col-lg-12 current_label_w">
            <el-form-item label="产品动销阈值">
              <el-select
                v-model="formOptions.thresholdValue"
                placeholder="请选择"
                @change="createData()"
              >
                <el-option
                  v-for="(item,index) in formOptions.thresholdValueData"
                  :key="index"
                  :value="item.value"
                  :label="item.name"
                />
              </el-select>
            </el-form-item>
          </div>
          <div class="selectOptions">
            <div class="selectOptions_l">基础参数</div>
            <div class="selectOptions_r">
              <div class="select_item_box">
                <div class="items_list">
                  <div class="label_box">课程所属年份</div>
                  <div class="select_checkbox_box">
                    <el-checkbox-group v-model="formOptions.year">
                      <el-checkbox
                        v-for="(item,index) in formOptions.yearData"
                        :key="index"
                        :label="item"
                        @change="createData()"
                      >{{ item }}</el-checkbox>
                    </el-checkbox-group>
                  </div>
                </div>
                <div class="items_list">
                  <div class="label_box">课程所属年级</div>
                  <div class="select_checkbox_box">
                    <el-checkbox-group v-model="formOptions.gradeId">
                      <el-checkbox
                        v-for="(item,index) in formOptions.gradeIdData"
                        :key="index"
                        :label="item.DictGuid"
                        @change="createData()"
                      >{{ item.SysDictValue }}</el-checkbox>
                    </el-checkbox-group>
                  </div>
                </div>
                <div class="items_list">
                  <div class="label_box">课程所属类型</div>
                  <div class="select_checkbox_box">
                    <el-checkbox-group v-model="formOptions.categoryId">
                      <el-checkbox
                        v-for="(item,index) in formOptions.categoryIdData"
                        :key="index"
                        :label="item.DictGuid"
                        @change="createData()"
                      >{{ item.SysDictValue }}</el-checkbox>
                    </el-checkbox-group>
                  </div>
                </div>
                <div class="items_list">
                  <div class="label_box">课程所属科目</div>
                  <div class="select_checkbox_box">
                    <el-checkbox-group v-model="formOptions.subjectId">
                      <el-checkbox
                        v-for="(item,index) in formOptions.subjectIdData"
                        :key="index"
                        :label="item.DictGuid"
                        @change="createData()"
                      >{{ item.SysDictValue }}</el-checkbox>
                    </el-checkbox-group>
                  </div>
                </div>
                <div class="items_list">
                  <div class="label_box">课程所属期段</div>
                  <div class="select_checkbox_box">
                    <el-checkbox-group v-model="formOptions.termId">
                      <el-checkbox
                        v-for="(item,index) in formOptions.termIdData"
                        :key="index"
                        :label="item.DictGuid"
                        @change="createData()"
                      >{{ item.SysDictValue }}</el-checkbox>
                    </el-checkbox-group>
                  </div>
                </div>
                <div class="items_list">
                  <div class="label_box">课程所属班型</div>
                  <div class="select_checkbox_box">
                    <el-checkbox-group v-model="formOptions.classTypeId">
                      <el-checkbox
                        v-for="(item,index) in formOptions.classTypeIdData"
                        :key="index"
                        :label="item.DictGuid"
                        @change="createData()"
                      >{{ item.SysDictValue }}</el-checkbox>
                    </el-checkbox-group>
                  </div>
                </div>
              </div>
              <div v-if="isShowPre" class="dialog_box_tip">
                注意：预计生成{{ ExistExtractorCount+ CreateExtractorCount }}个提取器，
                <span
                  @click="preview"
                >点击预览</span>
              </div>
            </div>
          </div>
          <div
            class="el-col el-col-sm-24 el-col-md-24 el-col-lg-24"
            style="text-align: center;margin-top: 20px"
          >
            <el-form-item>
              <el-button type="primary" size="medium" :disabled="isShowMessage" @click="onSubmit">提交</el-button>
              <el-button size="medium" @click="cancle">取消</el-button>
            </el-form-item>
          </div>
        </el-form>
      </el-col>
      <div class="dialogTable">
        <el-dialog title="提取器预览" :visible.sync="dialogTable.dialogTableVisible" width="70%">
          <div style="margin-bottom: 20px">
            <p
              class="p_title"
            >注意：当前选择会创建{{ ExistExtractorCount+ CreateExtractorCount }}个提取器,{{ CreateExtractorCount }}个新建，{{ ExistExtractorCount }}个已存在</p>
            <el-table :data="dialogTable.extractProductData" border>
              <el-table-column property="extractorNo" label="提取器编号" width="100">
                <template scope="scope">{{ scope.row.extractorNo?scope.row.extractorNo:"-" }}</template>
              </el-table-column>
              <el-table-column property="extractorStatusName" label="状态">
                <template
                  scope="scope"
                >{{ scope.row.extractorNo?scope.row.extractorStatusName:"-" }}</template>
              </el-table-column>

              <el-table-column property="fromSystemName" label="系统来源" width="150" />
              <el-table-column
                property="oneOrgName"
                label="所属事业部"
                width="200"
                show-overflow-tooltip
              />
              <el-table-column property="proStatueName" label="产品状态" />
              <el-table-column property="thresholdName" label="产品动销阈值" width="200" />
              <el-table-column property="year" label="所属年份" />
              <el-table-column property="gradeName" label="所属年级" />
              <el-table-column property="categoryName" label="所属类型" />
              <el-table-column property="subjectName" label="所属科目" />
              <el-table-column property="termName" label="所属期段" />
              <el-table-column property="classTypeName" label="所属班型" />
            </el-table>
          </div>

          <div slot="footer" class="dialog-footer" style="text-align: center">
            <el-button type="primary" @click="dialogTable.dialogTableVisible = false">关闭</el-button>
          </div>
        </el-dialog>
      </div>
    </div>
  </div>
</template>

<script>
import api from '@/api/poc_extractor'
import common from '@/utils/common'
import { showLoading, hideLoading } from '@/utils/loading'
import { mapGetters } from 'vuex'

export default {
  data() {
    return {
      formOptions: {
        fromSystem: [], // 系统来源
        fromSystemData: [], // 系统来源
        oneOrgId: [], // 所属事业部
        oneOrgIdData: [], // 所属事业部
        proStatue: '', // 产品状态
        proStatueData: [{ name: '全部', value: '-2' }, { name: '有效', value: '1' }, { name: '无效', value: '0' }],
        thresholdValue: '', // 产品动销阈值
        thresholdValueData: [{ name: '不限', value: '1000' }, { name: '3个月内', value: '2000' }, { name: '6个月内', value: '3000' }, { name: '1年内', value: '4000' }, { name: '2年内', value: '5000' }],
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
      dialogTable: {
        dialogTableVisible: false,
        extractProductData: [
        ],
        currentPage: '1'
      },
      pocSource: [],
      isShowMessage: false,
      baseDataArr: [],
      isProductTypeMapping: true,
      isBaseDataMapping: true,
      isShowPre: false

    }
  },
  computed: {
    // 已存在的提取器数据
    ExistExtractorCount: function() {
      return this.dialogTable.extractProductData.filter((x) => { return x.extractorNo.length > 0 }).length
    },
    CreateExtractorCount: function() {
      return this.dialogTable.extractProductData.filter((x) => { return x.extractorNo.length <= 0 }).length
    },
    ...mapGetters(['sidebar', 'userInfo'])
  },
  created() {
    showLoading()
    this.initFromSystem().then(() => {
      this.initBaseInfo().then(() => {
        this.checkMapping().then(() => {
          hideLoading()
        })
      })
    })
  },
  methods: {
    async initBaseInfo() {
      var that = this
      var request = { ParentGuid: ['2E9AED60-7EF3-477D-ABE3-0541F7DA3501', 'D7459004-5E26-43C7-B066-7F93F29D9A34', '1A930526-9F51-41BC-AC35-C890D0B60918', '0E0E674D-BBDF-4C3E-85AE-362E8FA5F3D5', 'AA98545B-495F-46FE-AA90-25C418E96C8C'] }
      var res = await api.getDict(request)
      if (res.Code === '0') {
        that.baseDataArr = res.Data.dataList
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
      this.pocSource = this.formOptions.fromSystemData.map(function(elem, index) { return elem.SysDictKey })
    },

    async  initFromSystem() {
      var request = { ParentGuid: ['C4AC5D00-0EDC-4B76-95AF-AC1EB1002A01'] }
      var res = await api.getDict(request)
      if (res.Code === '0') {
        this.formOptions.fromSystemData = res.Data.dataList
      }
    },
    fromSystemChange() {
      var that = this
      this.initoneOrg()
      this.createData()
      this.pocSource = that.formOptions.fromSystemData.filter((ele) => { return that.formOptions.fromSystem.indexOf(ele.DictGuid) >= 0 }).map(function(elem, index) { return elem.SysDictKey })
    },
    // 事业部
    initoneOrg() {
      this.formOptions.oneOrgId = []
      if (this.formOptions.fromSystem.length > 0) {
        var request = { ParentGuid: this.formOptions.fromSystem }
        if (this.formOptions.fromSystem) {
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
    // 验证所有基础参数是否映射参数
    async checkMapping() {
      // 判断产品池所有产品类型是否全部映射
      var that = this
      var res1 = await api.productTypeIsAllMapping({})
      if (res1.Data === false) {
        that.isProductTypeMapping = false
      }
      var res2 = await api.baseDataIsAllMapping({})
      if (res2.Data === false) {
        that.isBaseDataMapping = false
      }
      // var request = { pocSource: that.pocSource, fkDictGuid: that.baseDataArr.map(function(elem, index) { return elem.DictGuid }) }
      // var res2 = await api.getBaseData(request)
      // if (res2.Code === '0' && res2.Data.length > 0) {
      //   that.baseDataArr.forEach(function(item) {
      //     if (res2.Data.filter((ele) => { return ele.FKDictGuid === item.DictGuid }).length <= 0) {
      //       that.isMapping = false
      //     }
      //   })
      if (that.isBaseDataMapping === false || that.isProductTypeMapping === false) {
        that.isShowMessage = true
      } else {
        that.isShowMessage = false
      }
    },

    // 生成提取器
    createData() {
      var that = this
      that.dialogTable.extractProductData = []
      if (this.formOptions.year.length > 0 && this.formOptions.gradeId.length > 0 && this.formOptions.categoryId.length > 0 && this.formOptions.subjectId.length > 0 && this.formOptions.termId.length > 0 && this.formOptions.classTypeId.length > 0 && this.formOptions.fromSystem.length > 0 &&
        this.formOptions.oneOrgId.length > 0 && this.formOptions.proStatue && this.formOptions.thresholdValue) {
        var dataArr = common.cartesianProductOf(this.formOptions.year, this.formOptions.gradeId, this.formOptions.categoryId, this.formOptions.subjectId, this.formOptions.termId, this.formOptions.classTypeId)
        var fromSystemName = that.formOptions.fromSystemData.filter((ele) => that.formOptions.fromSystem.filter((x) => { return x === ele.DictGuid }).length > 0).map(function(elem, index) { return elem.SysDictValue }).join(',')
        var oneOrgName = that.formOptions.oneOrgIdData.filter((ele) => that.formOptions.oneOrgId.filter((x) => { return x === ele.DictGuid }).length > 0).map(function(elem, index) { return elem.SysDictValue }).join(',')
        var proStatueName = that.formOptions.proStatueData.filter((ele) => { return ele.value === that.formOptions.proStatue })[0].name
        var thresholdName = that.formOptions.thresholdValueData.filter((ele) => { return ele.value === that.formOptions.thresholdValue })[0].name
        dataArr.forEach(function(item) {
          var obj = {}
          obj.extractorNo = ''
          obj.extractorStatus = 2100
          obj.productCount = 10
          obj.extractorStatusName = '待提取'
          obj.fromSystemName = fromSystemName
          obj.fromSystem = that.formOptions.fromSystem
          obj.oneOrgName = oneOrgName
          obj.oneOrgId = that.formOptions.oneOrgId
          obj.proStatueName = proStatueName
          obj.proStatue = that.formOptions.proStatue
          obj.proStatueName = proStatueName
          obj.thresholdName = thresholdName
          obj.thresholdValue = that.formOptions.thresholdValue
          obj.year = item[0]
          obj.yearName = item[0]
          obj.gradeId = item[1]
          obj.gradeName = that.formOptions.gradeIdData.filter((ele) => { return ele.DictGuid === item[1] })[0].SysDictValue
          obj.categoryId = item[2]
          obj.categoryName = that.formOptions.categoryIdData.filter((ele) => { return ele.DictGuid === item[2] })[0].SysDictValue
          obj.subjectId = item[3]
          obj.subjectName = that.formOptions.subjectIdData.filter((ele) => { return ele.DictGuid === item[3] })[0].SysDictValue
          obj.termId = item[4]
          obj.termName = that.formOptions.termIdData.filter((ele) => { return ele.DictGuid === item[4] })[0].SysDictValue
          obj.classTypeId = item[5]
          obj.classTypeName = that.formOptions.classTypeIdData.filter((ele) => { return ele.DictGuid === item[5] })[0].SysDictValue
          obj.updateUserId = that.userInfo.UserGuid
          obj.updateUserName = that.userInfo.Name
          that.dialogTable.extractProductData.push(obj)
        })
      }
      if (that.dialogTable.extractProductData.length > 0) {
        that.isShowPre = true
      } else {
        that.isShowPre = false
      }
    },
    // 预览
    async preview() {
      var request = { dataList: this.dialogTable.extractProductData }
      if (this.dialogTable.extractProductData.length <= 100) {
        // 提取器预览
        showLoading()
        api.previewJudge(request).then(res => {
          if (res.Code === '0') {
            this.dialogTable.extractProductData = res.Data.dataList
            this.dialogTable.dialogTableVisible = true
          }
          hideLoading()
        })
      } else {
        common.toast('提取器数量超过100条,无法预览', 'error')
      }
    },
    // 提交
    async  onSubmit() {
      var that = this
      // 提交再判断一次数据是否全部映射
      showLoading()
      var res4 = await api.productTypeIsAllMapping({})
      if (res4.Data === false) {
        that.isProductTypeMapping = false
      }
      var res5 = await api.baseDataIsAllMapping({})
      if (res5.Data === false) {
        that.isBaseDataMapping = false
      }
      if (that.isBaseDataMapping === false || that.isProductTypeMapping === false) {
        that.isShowMessage = true
        hideLoading()
        return false
      } else {
        that.isShowMessage = false
      }
      if (this.dialogTable.extractProductData.length > 100) {
        hideLoading()
        common.toast('提取器数量超过100条,无法提交', 'error')
        return false
      }
      if (this.checkForm()) {
        const fromSystemsArr = that.formOptions.fromSystemData.filter((ele) => { return that.formOptions.fromSystem.indexOf(ele.DictGuid) >= 0 }).map(function(elem, index) { return elem.SysDictKey })
        const oneOrgIdsArr = that.formOptions.oneOrgIdData.filter((ele) => { return that.formOptions.oneOrgId.indexOf(ele.DictGuid) >= 0 }).map(function(elem, index) { return elem.SysDictKey })
        // 获取所有映射参数
        var reqeust1 = { pocSource: fromSystemsArr, fkDictGuid: that.formOptions.gradeId.concat(that.formOptions.categoryId).concat(that.formOptions.termId).concat(that.formOptions.subjectId).concat(that.formOptions.classTypeId) }
        var res1 = await api.getBaseData(reqeust1)
        var baseData = []
        if (res1.Code === '0') {
          baseData = res1.Data
        }
        // 判断每个提取器的产品数量
        for (const item of this.dialogTable.extractProductData) {
          const condition = {}
          condition.isCount = true
          condition.fromSystems = fromSystemsArr
          condition.oneOrgIds = oneOrgIdsArr
          condition.courseStatus = item.proStatue
          condition.thresholdValue = item.thresholdValue
          condition.courseYear = item.year
          condition.gradeId = []
          condition.categoryId = []
          condition.subjectId = []
          condition.termId = []
          condition.classTypeId = []
          condition.pageindex = 1
          condition.pageSize = 10000
          baseData.forEach((item1) => {
            switch (item1.FKDictGuid) {
              case item.gradeId:
                condition.gradeId.push(item1.DictId)
                break
              case item.categoryId:
                condition.categoryId.push(item1.DictId)
                break
              case item.termId:
                condition.termId.push(item1.DictId)
                break
              case item.subjectId:
                condition.subjectId.push(item1.DictId)
                break
              case item.classTypeId:
                condition.classTypeId.push(item1.DictId)
                break
            }
          })
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
          var res2 = await api.getCouseListByExtractor(condition)
          if (res2.Code === '0' && res2.Data.totalCount > 0) {
            item.ProductCount = res2.Data.totalCount
            item.ExtractorStatus = 2100
          } else {
            item.ProductCount = 0
            item.ExtractorStatus = 2300
          }
        }
        var request = { dataList: this.dialogTable.extractProductData }
        // 创建提取器
        var res3 = await api.createExtractor(request)
        if (res3.Code === '0') {
          hideLoading()
          common.toast('创建成功', 'success')
          this.$router.push({ path: '/ext_product_pool/poc_extractor' })
        } else {
          hideLoading()
          common.toast('创建失败', 'error')
        }
        hideLoading()
      } else {
        hideLoading()
      }
    },
    // 验证表单
    checkForm() {
      if (this.formOptions.fromSystem.length <= 0) {
        common.toast('请选择系统来源', 'error')
        return false
      }
      if (this.formOptions.oneOrgId.length <= 0) {
        common.toast('请选择事业部', 'error')
        return false
      }

      if (this.formOptions.proStatue.length <= 0) {
        common.toast('请选择产品状态', 'error')
        return false
      }
      if (this.formOptions.thresholdValue.length <= 0) {
        common.toast('请选择产品动销阈值', 'error')
        return false
      }
      if (this.formOptions.year.length <= 0) {
        common.toast('请选择年份', 'error')
        return false
      }
      if (this.formOptions.gradeId.length <= 0) {
        common.toast('请选择年级', 'error')
        return false
      }
      if (this.formOptions.categoryId.length <= 0) {
        common.toast('请选择所属类型', 'error')
        return false
      }
      if (this.formOptions.subjectId.length <= 0) {
        common.toast('请选择科目', 'error')
        return false
      }
      if (this.formOptions.termId.length <= 0) {
        common.toast('请选择所属期段', 'error')
        return false
      }
      if (this.formOptions.classTypeId.length <= 0) {
        common.toast('请选择班型', 'error')
        return false
      }
      return true
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
.poc_content_tit {
  border-left: 3px solid #409eff;
  padding-left: 10px;
  color: #409eff;
  font-size: 16px;
}
.poc_add_container {
  width: 70%;
  margin: 0 auto;
}
.poc_add_container .p_text_tip {
  color: red;
  font-size: 14px;
  margin-top: 40px;
}
.selectOptions_l {
  width: 100px;
  float: left;
  color: #606266;
  font-size: 14px;
  font-weight: 700;
}
.selectOptions_r {
  width: 87%;
  float: left;
}
.selectOptions_r .select_item_box {
  padding: 30px 0 20px 30px;
  width: 100%;
  float: left;
  background: #fafafa;
  border: 1px solid #eaeefb;
  margin-bottom: 20px;
}
.selectOptions_r .label_box {
  width: 100px;
  float: left;
  font-size: 14px;
}
.selectOptions_r .select_checkbox_box {
  width: 83%;
  float: left;
  padding-left: 20px;
}
.items_list {
  width: 100%;
  float: left;
  line-height: 34px;
  margin-bottom: 10px;
}
.dialog_box_tip {
  width: 100%;
  clear: both;
  margin-top: 20px;
  color: red;
}
.dialog_box_tip span {
  text-decoration: underline;
  cursor: pointer;
}
.p_title {
  padding-left: 5px;
  margin-top: 10px;
  color: red;
  margin-bottom: 10px;
}
.muiltAddBox >>> .el-form-item__label {
  width: 100px;
  text-align: left;
}

  .muiltAddBox >>> .el-select{
    width: 275px;
  }
</style>

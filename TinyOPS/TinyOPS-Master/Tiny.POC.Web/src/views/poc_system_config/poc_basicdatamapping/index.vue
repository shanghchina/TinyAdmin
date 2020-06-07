<!--系统设置-基础参数映射-->
<template>
  <div class="dashboard-container">
    <el-col :span="24">
      <el-form :inline="true" :model="formOptions" class="basicdatamappingBox">
        <SystemSource @childFn="getSystemSourceData" />
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6 labelWidth">
          <el-form-item label="是否映射">
            <el-select v-model="formOptions.State" placeholder="请选择" collapse-tags>
              <el-option label="全部" value="-1" />
              <el-option label="是" value="1" />
              <el-option label="否" value="0" />
            </el-select>
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6 marginTop">
          <el-form-item>
            <el-button v-if="isExitPermissionPoint('POC_BasicDataMap_Query')" type="primary" @click="handleSearch">查询</el-button>
          </el-form-item>
        </div>
      </el-form>
      <div class="table_container">
        <table>
          <thead>
            <tr>
              <td>系统来源</td>
              <td>所属事业部</td>
              <td>参数类型</td>
              <td>参数值</td>
              <td>是否映射</td>
              <td>映射基础参数</td>
              <td>映射时间</td>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item,index) in allSelectData" :key="index">
              <td>{{ item.FromSystem }}</td>
              <td>{{ item.OneOrgName }}</td>
              <td>{{ item.DictTypeName }}</td>
              <td>{{ item.DictValue }}</td>
              <td>{{ item.IsMapping }}</td>
              <td width="400px" style="position: relative">
                <div class="selectPanelBox" @click="showPanelBox(index,item)">
                  {{ item.SysCatalogTitle===null?'':item.SysCatalogTitle.split('|')[0] }}
                  <p v-if="item.SysCatalogTitle===null" class="p_text">请选择映射基础参数项</p>
                </div>
                <div v-if="item.ishow" class="dialog_panel_box">
                  <P />
                  <p>当前选择：
                    {{ selctLableData }}
                  </p>
                  <div class="panel_content">
                    <el-cascader-panel ref="myCascaderPanel" v-model="selectCurrentData" :options="proClassOptions" :props="{ checkStrictly: true }" @change="HandleSelect(index)" />
                  </div>
                  <div slot="footer" class="dialog-footer" style="margin-top: 20px">
                    <el-button v-if="isExitPermissionPoint('POC_ProductTypeMap_Submit')" type="primary" @click="handelSumbit(index)">提交</el-button>
                    <el-button @click="handelCancel(index)">取 消</el-button>
                  </div>
                </div>

              </td>
              <td>{{ item.UpdateDate|formatDate('yyyy-MM-dd hh:mm:ss') }}</td>
            </tr>
          </tbody>
        </table>

      </div>
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
import Api_MappingList from '@/api/poc_basicdatamapping'
import Api_Dictionary from '@/api/poc_extractor'
import * as Loading from '@/utils/loading'

export default {
  components: {
    SystemSource
  },
  data() {
    return {
      userInfo: this.$store.getters.userInfo,
      formOptions: {
        pocSource: [],
        oneOrgId: [],
        State: '' // 产品分类状态

      },
      currentSysCatalogTitle: '',
      allSelectData: [],
      currentPage: 1,
      pagesizes: [10, 20, 30, 40],
      pagesize: 10,
      total: 0,
      dialogPanelVisible: false, // dialog
      proClassOptions: [
        {
          value: '2E9AED60-7EF3-477D-ABE3-0541F7DA3501',
          label: '课程所属年级',
          children: [

          ]
        },
        {
          value: 'D7459004-5E26-43C7-B066-7F93F29D9A34',
          label: '课程所属类型',
          children: [
          ]
        },
        {
          value: '1A930526-9F51-41BC-AC35-C890D0B60918',
          label: '课程所属科目',
          children: [
          ]
        },
        {
          value: '0E0E674D-BBDF-4C3E-85AE-362E8FA5F3D5',
          label: '课程所属期段',
          children: [
          ]
        },
        {
          value: 'AA98545B-495F-46FE-AA90-25C418E96C8C',
          label: '课程所属班型',
          children: [
          ]
        }
      ],
      selectCurrentData: [], // 当前选中的值
      selctLableData: '', // 当前选中的label
      showIndex: -1,
      labelData: '',
      dataId: '' // 选中的当前行
    }
  },
  created() {
    this.initBaseInfo()
  },
  methods: {
    // 初始化基础参数
    async initBaseInfo() {
      Loading.showLoading()
      var that = this
      var request = { ParentGuid: ['2E9AED60-7EF3-477D-ABE3-0541F7DA3501', 'D7459004-5E26-43C7-B066-7F93F29D9A34', '1A930526-9F51-41BC-AC35-C890D0B60918', '0E0E674D-BBDF-4C3E-85AE-362E8FA5F3D5', 'AA98545B-495F-46FE-AA90-25C418E96C8C'] }
      var res = await Api_Dictionary.getDict(request)
      if (res.Code === '0') {
        res.Data.dataList.forEach(function(item) {
          switch (item.ParentGuid.toUpperCase()) {
            // grade
            case '2E9AED60-7EF3-477D-ABE3-0541F7DA3501':
              that.proClassOptions[0].children.push({ value: item.DictGuid, label: item.SysDictValue })
              break
              // flag
            case 'D7459004-5E26-43C7-B066-7F93F29D9A34':
              that.proClassOptions[1].children.push({ value: item.DictGuid, label: item.SysDictValue })
              break
              // subject
            case '1A930526-9F51-41BC-AC35-C890D0B60918':
              that.proClassOptions[2].children.push({ value: item.DictGuid, label: item.SysDictValue })
              break
              // term
            case '0E0E674D-BBDF-4C3E-85AE-362E8FA5F3D5':
              that.proClassOptions[3].children.push({ value: item.DictGuid, label: item.SysDictValue })
              break
              // classType
            case 'AA98545B-495F-46FE-AA90-25C418E96C8C':
              that.proClassOptions[4].children.push({ value: item.DictGuid, label: item.SysDictValue })
              break
          }
        })
      }
      Loading.hideLoading()
    },
    handleSearch() {
      if (this.formOptions.pocSource.length > 0) {
        this.currentPage = 1
        this.getBasicDataMapList()
      } else {
        this.common.toast('系统来源必填', 'error')
      }
    },
    showPanelBox(index, row) {
      if (this.showIndex >= 0 && this.showIndex !== index) {
        this.allSelectData[this.showIndex].ishow = false
      }
      this.showIndex = index
      this.allSelectData[index].ishow = true
      this.selectCurrentData = this.allSelectData[index].SysCatalogTitle === null ? '' : this.allSelectData[index].SysCatalogTitle.split('|')[1].split(',')
      this.selctLableData = row.SysCatalogTitle === null ? '' : row.SysCatalogTitle.split('|')[0]
      this.dataId = row.id
    },
    handelCancel(index) {
      this.allSelectData[index].ishow = false
    },
    handleCurrentChange(val) {
      if (this.formOptions.pocSource.length > 0) {
        this.currentPage = val
        this.getBasicDataMapList()
      } else {
        this.common.toast('系统来源必填', 'error')
      }
    },
    handleSizeChange(val) {
      if (this.formOptions.pocSource.length > 0) {
        this.showIndex = -1
        this.currentPage = 1
        this.pagesize = val
        this.getBasicDataMapList()
      } else {
        this.common.toast('系统来源必填', 'error')
      }
    },
    HandleSelect(index) {
      let arrLabel = this.$refs['myCascaderPanel'][0].getCheckedNodes()[0].pathLabels
      arrLabel = arrLabel.join('>')
      this.selctLableData = arrLabel
    },
    handelSumbit(index) {
      if (this.selctLableData) {
        if (this.selctLableData.split('>').length !== 2) {
          this.common.toast('请选择末级映射项', 'error')
        } else {
          Loading.showLoading()
          var request = {
            Id: this.allSelectData[index].Id,
            BasicDataMapGuid: this.allSelectData[index].BasicDataMapGuid,
            FKBasicDataGuid: this.allSelectData[index].BasicDataGuid,
            FKDictGuid: this.selectCurrentData[1],
            SysCatalogTitle: this.selectCurrentData.join(','),
            UpdaterUserId: this.userInfo.UserGuid,
            UpdaterUserName: this.userInfo.Name
          }
          Api_MappingList.operateBasicDataMap(request).then(response => {
            if (response.Code === '0') {
              this.common.toast(response.Message, 'success')
              this.getBasicDataMapList()
              this.allSelectData[index].ishow = false
            }
          })
          Loading.hideLoading()
        }
      } else {
        this.common.toast('请选择映射项', 'error')
      }
    },
    getBasicDataMapList() {
      Loading.showLoading()
      var request = {
        Source: this.formOptions.pocSource,
        LevelOneOrgID: this.formOptions.oneOrgId,
        IsMapping: this.formOptions.State,
        PageIndex: this.currentPage,
        PageSize: this.pagesize
      }
      Api_MappingList.getBasicDataMapList(request).then(response => {
        if (response.Code === '0') {
          response.Data.DataList.forEach(a => {
            a.ishow = false
          })
          this.allSelectData = response.Data.DataList
          this.total = response.Data.Total
        }
      })
      Loading.hideLoading()
    }, getSystemSourceData(val) {
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
  .selectPanelBox{
    width: 400px;
    height: 30px;
    line-height: 30px;
    border: 1px solid #ddd;
    display: inline-block;
    padding-left: 15px;
    text-align: left;
  }
  .p_text{
    color: #909399;
    margin:0
  }

  .table_container table tbody  tr td {
    border:1px solid #DCDFE6;
    padding: 10px;
    font-size: 14px;
    color: #606266;
  }
  .table_container table thead tr td {
    height: 51px;
    line-height: 51px;
    border:1px solid #DCDFE6;
    color: #909399;
    font-weight: 500;
    font-size: 14px;
  }
  .table_container{
    /*overflow-x: scroll;*/
    /*width: 1600px;*/
    /*display:block;*/
    clear: both;
    overflow:auto
  }
  .table_container table thead{
    text-align: center;
  }
  .table_container table {
    width: 1600px;
    border-collapse: collapse;
  }
  .dialog_panel_box{
    z-index: 999;
    padding:0 20px 20px;
    width: 400px;
    margin-top: 5px;
    box-shadow: 0 2px 12px 0 rgba(0,0,0,.1);
    border-radius: 4px;
    border: 1px solid #e4e7ed;
    box-sizing: border-box;
    background-color: #fff;
     position: absolute;
  }
  .dialog_panel_box p{
    text-align: left;

  }
  /*.labelWidth >>>.el-form-item__label{*/
    /*width: 82px;*/
  /*}*/
  @media only screen and (max-width: 1430px){
    .basicdatamappingBox >>> .el-form-item__label{
      width: 100%;
      text-align: left;
    }
    .marginTop{
      margin-top: 40px;
    }
  }
  .paginationBox{
    float: left;
    width: 100%;
  }
</style>

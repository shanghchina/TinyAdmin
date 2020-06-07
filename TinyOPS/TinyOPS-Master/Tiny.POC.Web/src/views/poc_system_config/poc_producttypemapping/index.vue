<!--产品分类映射页面-->
<template>
  <div class="dashboard-container">
    <el-col :span="24">
      <el-form :inline="true" :model="formOptions" class="producttypemappingBox">
        <SystemSource @childFn="getSystemSourceData" />
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6 labelWidth">
          <el-form-item label="是否映射">
            <el-select v-model="formOptions.state" placeholder="请选择" collapse-tags>
              <el-option label="全部" value="-1" />
              <el-option label="是" value="1" />
              <el-option label="否" value="0" />
            </el-select>
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6 marginTop">
          <el-form-item>
            <el-button
              v-if="isExitPermissionPoint('POC_ProductTypeMap_Query')"
              type="primary"
              @click="handleSearch"
            >查询</el-button>
          </el-form-item>
        </div>
      </el-form>
      <div class="table_container">
        <table>
          <thead>
            <tr>
              <!--<td>Id</td>-->
              <td>系统来源</td>
              <td>所属事业部</td>
              <td>产品分类名称</td>
              <td>分类级数</td>
              <td>是否映射</td>
              <td>映射产品分类名称</td>
              <td>映射时间</td>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item,index) in allSelectData" :key="item.id">
              <!--<td>{{item.id}}</td>-->
              <td>{{ item.PocSource }}</td>
              <td>{{ item.LevelOneOrgName }}</td>
              <td>{{ item.ProductTypeName }}</td>
              <td>{{ item.NodeFlag }}</td>
              <td>{{ item.IsMapping }}</td>
              <td width="700px" style="position: relative">
                <div class="selectPanelBox" @click="showPanelBox(index,item)">
                  {{ item.ProductTypeTitle }}
                  <p v-if="item.ProductTypeTitle===null" class="p_text">请选择产品映射分类</p>
                </div>
                <div v-if="item.Ishow" class="dialog_panel_box">
                  <P />
                  <p>
                    当前选择：
                    {{ selctLableData }}
                  </p>
                  <div class="panel_content">
                    <el-cascader-panel
                      ref="myCascaderPanel"
                      v-model="selectCurrentData"
                      :options="productTypeLevel"
                      :props="{ checkStrictly: true }"
                      @change="handleSelect"
                    />
                  </div>
                  <div slot="footer" class="dialog-footer" style="margin-top: 20px">
                    <el-button
                      v-if="isExitPermissionPoint('POC_ProductTypeMap_Submit')"
                      type="primary"
                      @click="handelSumbit(index)"
                    >提交</el-button>
                    <el-button @click="handelCancel(index)">取 消</el-button>
                  </div>
                </div>
              </td>
              <td>{{ item.MappingDateDate|formatDate('yyyy-MM-dd hh:mm:ss') }}</td>
            </tr>
          </tbody>
        </table>
      </div>
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
import * as api from '@/api/poc_producttypemapping'
import * as Loading from '@/utils/loading'
export default {
  components: {
    SystemSource
  },
  data() {
    return {
      userInfo: this.$store.getters.userInfo,
      formOptions: {
        pocSource: [], // 系统来源
        oneOrgId: [], // 所属事业部
        state: '' // 产品分类状态
      },
      tableData: [],
      currentproMapClassName: '',
      allSelectData: [],
      currentPage: 1,
      pagesizes: [10, 20, 30, 40],
      pagesize: 10,
      total: 0,
      dialogPanelVisible: false, // dialog
      productTypeLevel: [],
      selectCurrentData: [], // 当前选中的值
      selctLableData: '', // 当前选中的label
      labelData: '',
      showIndex: -1,
      isshowtip: true, //
      dataId: '' // 选中的当前行
    }
  },
  created() {
    this.getProductTypeLevel()
  },
  methods: {
    async getTableData() {
      Loading.showLoading()
      const data = await api.GetPageInfoList({
        PocSource: this.formOptions.pocSource,
        LevelOneOrgID: this.formOptions.oneOrgId,
        IsMapping: this.formOptions.state,
        PageIndex: this.currentPage,
        PageSize: this.pagesize
      })
      if (data.Code === '0') {
        data.Data.DataList.forEach(_ => {
          _.ProductTypeTitleId = _.ProductTypeTitle === null ? null : _.ProductTypeTitle.split('|')[1].split(',')
          _.ProductTypeTitle = _.ProductTypeTitle === null ? null : _.ProductTypeTitle.split('|')[0]
        })
        this.allSelectData = data.Data.DataList
        this.total = data.Data.Total
      }
      Loading.hideLoading()
    },
    async getProductTypeLevel() {
      const ptyLevel = await api.GetProductTypeLevel()
      if (ptyLevel.Code === '0') {
        this.productTypeLevel = ptyLevel.Data
      }
    },
    handleSearch() {
      if (this.formOptions.pocSource.length > 0) {
        this.currentPage = 1
        this.getTableData()
      } else {
        this.common.toast('系统来源必填', 'error')
      }
    },
    showPanelBox(index, row) {
      if (this.showIndex >= 0 && this.showIndex !== index) {
        this.allSelectData[this.showIndex].Ishow = false
      }
      this.showIndex = index
      this.allSelectData[index].Ishow = true
      this.selectCurrentData = row.ProductTypeTitleId
      this.selctLableData = row.ProductTypeTitle
      this.dataId = row.id
    },
    handelCancel(index) {
      this.allSelectData[index].Ishow = false
    },
    handleCurrentChange(val) {
      if (this.formOptions.pocSource.length <= 0) {
        this.common.toast('系统来源必填', 'error')
      } else {
        this.currentPage = val
        this.getTableData()
      }
    },
    handleSizeChange(val) {
      if (this.formOptions.pocSource.length <= 0) {
        this.common.toast('系统来源必填', 'error')
      } else {
        this.showIndex = -1
        this.currentPage = 1
        this.pagesize = val
        this.getTableData()
      }
    },
    handleSelect() {
      let arrLabel = this.$refs['myCascaderPanel'][0].getCheckedNodes()[0]
        .pathLabels
      arrLabel = arrLabel.join('>')
      this.selctLableData = arrLabel
    },
    async handelSumbit(index) {
      if (!this.selectCurrentData) {
        this.common.toast('请正确选择映射产品分类', 'error')
        return
      }
      Loading.showLoading()
      this.allSelectData[index].Ishow = false
      const thisrowdata = this.allSelectData.filter(s => s.id === this.dataId)
      const parameter = {
        ProductTypeMapGuid: this.allSelectData[index].ProductTypeMapGuid,
        FKItemTypeId: this.allSelectData[index].Id,
        FKProductTypeGuid: this.selectCurrentData.slice(-1)[0],
        ProductTypeLevelNo: this.selectCurrentData.length,
        ProductTypeID: this.allSelectData[index].ProductTypeID,
        ProductTypeTitle: this.selectCurrentData.join(','),
        UpdaterUserId: this.userInfo.UserGuid,
        UpdaterUserName: this.userInfo.Name,
        CreatedDate: new Date().Format('yyyy-MM-dd HH:mm:ss')
      }
      const isSuccess = await api.AddOrUpdateProductTypeMap(parameter)
      if (isSuccess.Code === '0') {
        await this.common.toast('提交成功', 'success')
        thisrowdata[index].ProductTypeTitle = this.selctLableData
        thisrowdata[index].ProductTypeTitleId = this.selectCurrentData
        thisrowdata[index].MappingDateDate = parameter.CreatedDate
      }
      Loading.hideLoading()
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
.selectPanelBox {
  width: 650px;
  height: 30px;
  line-height: 30px;
  border: 1px solid #ddd;
  display: inline-block;
  padding-left: 15px;
  text-align: left;
}
.p_text {
  color: #909399;
  margin: 0;
}

.table_container table tbody tr td {
  border: 1px solid #dcdfe6;
  padding: 10px;
  font-size: 14px;
  color: #606266;
}
.table_container table thead tr td {
  height: 51px;
  line-height: 51px;
  border: 1px solid #dcdfe6;
  color: #909399;
  font-weight: 500;
  font-size: 14px;
}
.table_container {
  /*overflow-x: scroll;*/
  /*width: 1600px;*/
  /*display:block;*/
  clear: both;
  overflow: auto;
}
.table_container table thead {
  text-align: center;
}
.table_container table {
  width: 1600px;
  border-collapse: collapse;
}
.dialog_panel_box {
  position: absolute;
  z-index: 999;
  padding: 0 20px 20px;
  width: 650px;
  margin-top: 5px;
  box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);
  border-radius: 4px;
  border: 1px solid #e4e7ed;
  box-sizing: border-box;
  background-color: #fff;
}
.dialog_panel_box p {
  text-align: left;
}
/*.labelWidth >>> .el-form-item__label {*/
/*width: 82px;*/
/*}*/

@media only screen and (max-width: 1434px) {
  .producttypemappingBox >>> .el-form-item__label {
    width: 100%;
    text-align: left;
  }
  .marginTop {
    margin-top: 40px;
  }
}
</style>

<!--系统设置-后台维护-->
<template>
  <div class="dashboard-container">
    <el-col :span="24">
      <el-form :inline="true" :model="formOptions" class="poc_systemsetupBox">
        <SystemSource @childFn="getSystemSourceData" />
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6">
          <el-form-item label="更新日期">
            <el-date-picker
              v-model="formOptions.updateTime"
              type="date"
              value-format="yyyy-MM-dd"
              placeholder="选择日期"
            />
          </el-form-item>
        </div>
      </el-form>
      <div class="el-col el-col-sm-12 el-col-md-24 el-col-lg-24">
        <el-table :data="tableData" border style="width: 401px" empty-text=" ">
          <el-table-column
            prop="settingName"
            label="获取产品体系类别"
            width="200"
          />
          <el-table-column
            v-if="false"
            prop="settingVal"
          />
          <el-table-column
            label="操作"
            width="200"
          >
            <template slot-scope="scope">
              <el-button
                size="mini"
                type="primary"
                @click="handleClick(scope.$index, scope.row)"
              >开始获取</el-button>

            </template>
          </el-table-column>
        </el-table>
      </div>
    </el-col>
  </div>
</template>

<script>
import SystemSource from '@/components/SystemSource'
import * as api from '@/api/poc_systemsetup'
export default {
  components: {
    SystemSource
  },
  data() {
    return {
      formOptions: {
        pocSource: '',
        oneOrgId: '',
        updateTime: ''
      },
      tableData: [{
        settingName: '获取校管家-产品大类表',
        settingVal: '1000'
      }, {
        settingName: '获取校管家-课程信息表',
        settingVal: '2000'
      }, {
        settingName: '获取校管家-班级信息',
        settingVal: '3000'
      }, {
        settingName: '获取校管家-基础信息',
        settingVal: '4000'
      }, {
        settingName: '获取收入中心-阈值数据',
        settingVal: '5000'
      }]
    }
  },
  created() {
  },
  methods: {
    getSystemSourceData(val) {
      this.formOptions.pocSource = val.pocSource
      this.formOptions.oneOrgId = val.oneOrgId
    },
    handleClick(index, row) {
      // debugger
      const loading = this.$loading({
        lock: true,
        text: '正在获取数据中，请稍等...',
        background: 'hsla(0,0%,100%,.9)'
      })
      if (row.settingName.indexOf('阈值') > -1) {
        // 获取阈值
        this.GetThresholdData(loading)
      } else {
        // 获取产品体系/基础信息
        this.GetData(loading, row)
      }
    },
    // 获取阈值
    async GetThresholdData(loading) {
      if (this.formOptions.updateTime) {
        const data = { queryDate: this.formOptions.updateTime }
        await api.GetThresholdData(data).then(response => {
          if (response.Code === '0') {
            this.$message({
              type: 'success',
              message: '操作完成，正在后台处理获取阈值数据，请查看日志记录！'
            })
          } else {
            this.$message.error(response.Message)
          }
        })
      } else {
        this.$message.error('请选择日期！')
      }
      setTimeout(() => {
        loading.close()
      }, 500)
    },
    async GetData(loading, row) {
      if (this.formOptions.pocSource && this.formOptions.updateTime) {
        const data = {
          pocSource: this.formOptions.pocSource,
          levelOneOrgID: this.formOptions.oneOrgId,
          dataType: row.settingVal,
          queryDate: this.formOptions.updateTime
        }
        if (row.settingVal === '4000') {
          await api.GetBasicData(data).then(response => {
            if (response.Code === '0') {
              this.$message({
                type: 'success',
                message: '操作完成，正在后台处理获取基本数据，请查看日志记录！'
              })
            } else { this.$message.error(response.Message) }
          })
        } else {
          await api.GetProductData(data).then(response => {
            if (response.Code === '0') {
              this.$message({
                type: 'success',
                message: '操作完成，正在后台处理获取数，请查看日志记录！'
              })
            } else {
              this.$message.error(response.Message)
            }
          })
        }
      } else {
        this.$message.error('请选择日期和系统来源！')
      }
      setTimeout(() => {
        loading.close()
      }, 500)
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
  .div_box{
    width: 100%;
    display: inline-block;
   height: 62px;
  }
  .demo_form >>> .el-form-item__label{
    width:80px ;
    text-align: left;
  }
  @media only screen and (max-width: 1466px){
    .div_box{
      height: 102px;
   }
  }
  @media only screen and (max-width: 1466px) {
    .poc_systemsetupBox >>> .el-form-item__label{
      width: 100%;
      text-align: left;
    }
    .marginTop {
      margin-top: 40px;
    }
  }
</style>


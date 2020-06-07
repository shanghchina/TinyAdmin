<template>
  <div>
    <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6 labelWidth">
      <el-form-item label="系统来源">
        <el-select
          v-model="formSystem"
          placeholder="请选择"
          multiple
          collapse-tags
          @change="initOneOrg()"
        >
          <el-option
            v-for="item in formSystemData"
            :key="item.SysDictKey"
            :value="item"
            :label="item.SysDictValue"
          />
        </el-select>
      </el-form-item>
    </div>
    <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6 labelWidth">
      <el-form-item label="所属事业部">
        <el-select v-model="oneOrgId" placeholder="请选择" multiple collapse-tags @change="parentFn()">
          <el-option
            v-for="item in oneOrgIdData"
            :key="item.SysDictKey"
            :value="item.SysDictKey"
            :label="item.SysDictValue"
          />
        </el-select>
      </el-form-item>
    </div>
  </div>
</template>
<script>
import api from '@/api/poc_extractor'
export default {
  data() {
    return {
      formSystem: '',
      oneOrgId: '',
      formSystemData: [],
      oneOrgIdData: []
    }
  },
  created() {
    this.initFromSystem()
  },
  methods: {
    // 来源
    initFromSystem() {
      var request = { ParentGuid: ['C4AC5D00-0EDC-4B76-95AF-AC1EB1002A01'] }
      api.getDict(request).then(res => {
        if (res.Code === '0') {
          this.formSystemData = res.Data.dataList
        }
      })
    }, // 事业部
    initOneOrg() {
      this.$emit('childFn', {
        pocSource: this.formSystem.map(_ => _.SysDictKey),
        oneOrgId: this.oneOrgId
      })
      var request = { ParentGuid: this.formSystem.map(_ => _.DictGuid) }
      if (this.formSystem.length > 0) {
        api.getDict(request).then(res => {
          if (res.Code === '0') {
            this.oneOrgIdData = res.Data.dataList
          }
        })
      } else {
        this.oneOrgIdData = []
      }
      this.oneOrgId = ''
    },
    parentFn() {
      this.$emit('childFn', {
        pocSource: this.formSystem.map(_ => _.SysDictKey),
        oneOrgId: this.oneOrgId
      })
    }
  }
}
</script>

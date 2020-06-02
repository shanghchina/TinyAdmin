<!--提取器处理-提取器确认页面-->
<template>
  <div class="dashboard-container">
    <div class="el-col el-col-sm-24 el-col-md-24 el-col-lg-24 poc_content_tit">提取器处理</div>
    <div class="poc_add_container">
      <div class="step_panel">
        <el-steps  :active="active" >
          <el-step title="提取规则"></el-step>
          <el-step title="产品提取"></el-step>
          <el-step title="提取确认"></el-step>
          <el-step title="提取完成"></el-step>
        </el-steps>
      </div>
      <div class="container_box">
        <el-button type="success" icon="el-icon-check" circle></el-button>
        <p class="tip_text">提取完成</p>

      </div>
      <div style="text-align: center;margin-top: 130px"> <el-button type="primary" @click="goBack">返回列表</el-button></div>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      active: 4
    }
  },
  methods: {
    goBack() {
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
  .poc_add_container{
    width: 70%;
    margin: 0 auto;
  }
  .container_box{
    text-align: center;
  }
  .container_box .el-button {
    font-size:80px;
    margin-top: 20px;
  }
  .container_box .tip_text{
    font-size: 25px;
  }
</style>

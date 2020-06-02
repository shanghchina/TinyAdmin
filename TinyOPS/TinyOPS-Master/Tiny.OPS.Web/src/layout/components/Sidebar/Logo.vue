<!-- logo -->
<template>
  <div class="logo" @click="goPage()">
    <!--<router-link tag="a"  :to="{name:'dashboard'}">-->
    <transition name="fade">
      <span key="0" class="logo_title is-blod" :class="{'is-text':!type,'is-img':type}">
        <template v-if="type">
          <img :src="website.logo" width="30" height="30">
        </template>
        <template v-else>{{ website.logo }}</template>
      </span>
    </transition>
    <transition-group>
      <template v-if="!isCollapse" style="margin-left: -30px">
        <span key="2" class="logo_subtitle">{{ website.author }}</span>
      </template>
    </transition-group>
    <!--</router-link>-->
  </div>
</template>

<script>
export default {
  props: ['isCollapse'],
  data() {
    return {
      website: { logo: require('@/assets/favicon.png'), author: '产品中心' }
    }
  },
  computed: {
    type: function(val) {
      return this.website.logo.indexOf('static') !== -1
    }
  },
  created() {

  },
  methods: {
    goPage() {
      this.$router.push({ name: 'Home' })
    }
  }
}
</script>
<style lang='scss' scoped>
.fade-leave-active {
  transition: opacity 0.2s;
}
.fade-enter-active {
  transition: opacity 2.5s;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}
.logo {
  cursor: pointer;
  position: relative;
  display: flex;
  justify-content: center;
  align-items: center;
  // width: 230px;
  height: 64px;
  line-height: 64px;
  background: #263445;
  color: #fdfdfd;
  text-align: center;
  font-size: 20px;
  font-weight: 600;
  overflow: hidden;
  box-sizing: border-box;
}
.logo_title {
  padding: 0 5px 0 0;
  color: #42b983;
  font-size: 16px;
  &.is-bold {
    font-weight: 700;
  }
}
.is-img {
  position: absolute;
  top: 10px;
  left: 10px;
}
.logo_subtitle {
  font-size: 16px;
  padding-top: 5px;
  color: orangered;
}
</style>

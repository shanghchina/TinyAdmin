import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '@/layout'

/**
 * Note: sub-menu only appear when route children.length >= 1
 * Detail see: https://panjiachen.github.io/vue-element-admin-site/guide/essentials/router-and-nav.html
 *
 * hidden: true                   if set true, item will not show in the sidebar(default is false)
 * alwaysShow: true               if set true, will always show the root menu
 *                                if not set alwaysShow, when item has more than one children route,
 *                                it will becomes nested mode, otherwise not show the root menu
 * redirect: noRedirect           if set noRedirect will no redirect in the breadcrumb
 * name:'router-name'             the name is used by <keep-alive> (must set!!!)
 * meta : {
    roles: ['admin','editor']    control the page roles (you can set multiple roles)
    title: 'title'               the name show in sidebar and breadcrumb (recommend set)
    icon: 'svg-name'             the icon show in the sidebar
    breadcrumb: false            if set false, the item will hidden in breadcrumb(default is true)
    activeMenu: '/example/list'  if set path, the sidebar will highlight the path you set
  }
 */

/**
 * constantRoutes
 * a base page that does not have permission requirements
 * all roles can be accessed
 */
export const constantRoutes = [
  { path: '/login',
    meta: { title: '登录', noCache: true },
    component: () => import('@/views/login'),
    hidden: true
  },
  {
    path: '/404',
    component: () => import('@/views/404'),
    hidden: true
  },
  {
    path: '/',
    component: Layout,
    redirect: '/home',
    children: [{
      path: 'home',
      name: 'Home',
      component: () => import('@/views/home/index'),
      meta: { title: '首页', icon: 'dashboard' }
    }]
  }

]
export const noMenuRoutes = [
  {
    path: '/form',
    component: Layout,
    children: [
      {
        path: 'index',
        name: 'Form',
        component: () => import('@/views/form/index'),
        meta: { title: 'Form', icon: 'form' }
      }
    ]
  }
]
export const permissionRoutes = [
  {
    path: '/ext_product_pool',
    component: Layout,
    name: 'ext_product_pool',
    meta: { title: '事业部产品池', icon: 'table', permissionsAttributes: 'EXT_Product_Pool_Menu' },
    children: [
      {
        path: 'ext_basicdata',
        name: 'ext_basicdata',
        component: () => import('@/views/ext_product_pool/ext_basicdata/index'),
        meta: { title: '基础参数', permissionsAttributes: 'EXT_BasicData_Page' }
      },
      {
        path: 'ext_productitemtype',
        name: 'ext_productitemtype',
        component: () => import('@/views/ext_product_pool/ext_productitemtype/index'),
        meta: { title: '产品分类', permissionsAttributes: 'EXT_ProductItemType_Page' }
      },
      {
        path: 'ext_productcourse',
        name: 'ext_productcourse',
        component: () => import('@/views/ext_product_pool/ext_productcourse/index'),
        meta: { title: '课程管理', permissionsAttributes: 'EXT_ProductCourse_Page' }
      },
      {
        path: 'ext_productclass',
        name: 'ext_productclass',
        component: () => import('@/views/ext_product_pool/ext_productclass/index'),
        meta: { title: '班级管理', permissionsAttributes: 'EXT_ProductClass_Page' }
      },
      {
        path: 'poc_extractor',
        name: 'poc_extractor',
        component: () => import('@/views/ext_product_pool/poc_extractor/index'),
        meta: { title: '提取器管理', permissionsAttributes: 'POC_Extractor_Page' }
      },
      {
        path: 'poc_extractor_muilt_add',
        name: 'poc_extractor_muilt_add',
        component: () => import('@/views/ext_product_pool/poc_extractor/poc_extractor_muilt_add')

      },
      {
        path: 'poc_extractor_step01',
        name: 'poc_extractor_step01',
        component: () => import('@/views/ext_product_pool/poc_extractor/poc_extractor_step01')

      },
      {
        path: 'poc_extractor_step02',
        name: 'poc_extractor_step02',
        component: () => import('@/views/ext_product_pool/poc_extractor/poc_extractor_step02')

      },
      {
        path: 'poc_extractor_step03',
        name: 'poc_extractor_step03',
        component: () => import('@/views/ext_product_pool/poc_extractor/poc_extractor_step03')

      },
      {
        path: 'poc_extractor_step04',
        name: 'poc_extractor_step04',
        component: () => import('@/views/ext_product_pool/poc_extractor/poc_extractor_step04')

      },
      {
        path: 'poc_extractor_detail',
        name: 'poc_extractor_detail',
        component: () => import('@/views/ext_product_pool/poc_extractor/poc_extractor_detail')

      }
    ]
  },
  {
    path: '/poc_product_manage',
    component: Layout,
    name: 'poc_product_manage',
    meta: { title: '产品管理', icon: 'example', permissionsAttributes: 'POC_Product_Manage_Menu' },
    children: [
      {
        path: 'poc_producttype',
        name: 'productcategoryone',
        component: () => import('@/views/poc_product_manage/poc_producttype/index'),
        meta: { title: '产品分类管理', permissionsAttributes: 'POC_ProductType_Page' }
      },
      {
        path: 'poc_productcourse',
        name: 'coursemanageone',
        component: () => import('@/views/poc_product_manage/poc_productcourse/index'),
        meta: { title: '产品管理', permissionsAttributes: 'POC_ProductCourse_Page' }
      }
    ]
  },
  {
    path: '/poc_system_config',
    component: Layout,
    name: 'poc_system_config',
    meta: { title: '系统设置', icon: 'form', permissionsAttributes: 'POC_System_Config_Menu' },
    children: [
      {
        path: 'poc_basicdata',
        name: 'poc_basicdata',
        component: () => import('@/views/poc_system_config/poc_basicdata/index'),
        meta: { title: '基础参数设置', permissionsAttributes: 'POC_BasicData_Page' }
      },
      {
        path: 'poc_producttypemapping',
        name: 'poc_producttypemapping',
        component: () => import('@/views/poc_system_config/poc_producttypemapping/index'),
        meta: { title: '产品分类映射', permissionsAttributes: 'POC_ProductTypeMap_Page' }
      },
      {
        path: 'poc_basicdatamapping',
        name: 'poc_basicdatamapping',
        component: () => import('@/views/poc_system_config/poc_basicdatamapping/index'),
        meta: { title: '基础参数映射', permissionsAttributes: 'POC_BasicDataMap_Page' }
      },
      {
        path: 'poc_systemsetup',
        name: 'poc_systemsetup',
        component: () => import('@/views/poc_system_config/poc_systemsetup/index'),
        meta: { title: '后台维护', permissionsAttributes: 'POC_SystemSetup_Page' }
      }
    ]
  }

]
export default new Router({
  // mode: 'history', // require service support
  scrollBehavior: () => ({ y: 0 }),
  routes: constantRoutes.concat(noMenuRoutes)
})


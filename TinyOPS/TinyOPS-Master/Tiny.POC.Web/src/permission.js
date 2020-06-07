import router from './router'
import NProgress from 'nprogress' // progress bar
import 'nprogress/nprogress.css' // progress bar style
import { getLoginId, setLoginId } from '@/utils/auth' // get token from cookie
import getPageTitle from '@/utils/get-page-title'
import store from '@/store'
import common from '@/utils/common'
import { permissionRoutes, constantRoutes } from './router'
import { getToken } from '@/utils/auth' // getToken from cookie

NProgress.configure({ showSpinner: false }) // NProgress Configuration

const whiteList = ['/login']// no redirect whitelist

function hasPermission(permissions, route) {
  if (route.meta && route.meta.permissionsAttributes) {
    const res = permissions
    const find = res.filter(item => item.indexOf(route.meta.permissionsAttributes) === 0)
    return find && find.length > 0
  } else {
    return true
  }
}

/**
 * 递归过滤异步路由表，返回符合用户角色权限的路由表
 * @param routes asyncRoutes
 * @param roles
 */
function filterAsyncRoutes(routes, roles) {
  const res = []
  routes.forEach(route => {
    const tmp = { ...route }
    if (hasPermission(roles, tmp)) {
      if (tmp.children) {
        tmp.children = filterAsyncRoutes(tmp.children, roles)
      }
      res.push(tmp)
    }
  })
  return res
}
// 动态获取菜单
function addRoutes() {
  var permissionPoints = store.state.user.userInfo.PermissionPoints
  var routesArr = filterAsyncRoutes(permissionRoutes, permissionPoints)
  router.options.routes = constantRoutes.concat(routesArr)
  router.addRoutes(routesArr)
  router.addRoutes([{ path: '*', redirect: '/404', hidden: true }])
}

// 测试所有菜单
function testAddRoutes() {
  // var permissionPoints = store.state.user.userInfo.PermissionPoints
  var routesArr = permissionRoutes
  router.options.routes = constantRoutes.concat(routesArr)
  router.addRoutes(routesArr)
  router.addRoutes([{ path: '*', redirect: '/404', hidden: true }])
}

// 判断是否存在权限点
export function isExitPermissionPoint(permissionPoint) {
  var permissionPoints = store.state.user.userInfo.PermissionPoints
  return permissionPoints.indexOf(permissionPoint) > -1
}

router.beforeEach((to, from, next) => {
  if (to.meta.title) {
    document.title = to.meta.title // + ' - ' + Config.title
  }
  NProgress.start()
  var username = getLoginId()
  setLoginId(username)

  if (getToken()) { // if (loginId) {
    // 已登录且要跳转的页面是登录页
    if (to.path === '/login') {
      next({ path: '/' })
      NProgress.done()
    } else {
      testAddRoutes()
      if (username.length !== 0) { // 判断当前用户是否已拉取完user_info信息
        store.dispatch('user/Get_SysUser', username).then(res => { // 拉取user_info
        }).catch((err) => {
          console.log(err)
          store.dispatch('user/LogOut').then(() => {
            // location.reload() // 为了重新实例化vue-router对象 避免bug
          })
        })
      } else {
        next()
      }

      // if (store.getters.roles.length === 0) { // 判断当前用户是否已拉取完user_info信息
      //   store.dispatch('GetInfo').then(res => { // 拉取user_info
      //     // 动态路由，拉取菜单
      //     loadMenus(next, to)
      //   }).catch((err) => {
      //     console.log(err)
      //     store.dispatch('LogOut').then(() => {
      //       location.reload() // 为了重新实例化vue-router对象 避免bug
      //     })
      //   })
      // // 登录时未拉取 菜单，在此处拉取
      // } else if (store.getters.loadMenus) {
      //   // 修改成false，防止死循环
      //   store.dispatch('updateLoadMenus').then(res => {})
      //   loadMenus(next, to)
      // } else {
      //   next()
      // }

      next()
    }
  } else {
    /* has no token*/
    if (whiteList.indexOf(to.path) !== -1) { // 在免登录白名单，直接进入
      next()
    } else {
      next(`/login?redirect=${to.path}`) // 否则全部重定向到登录页
      NProgress.done()
    }
  }
})

// export const loadMenus = (next, to) => {
//   buildMenus().then(res => {
//     const asyncRouter = filterAsyncRouter(res)
//     asyncRouter.push({ path: '*', redirect: '/404', hidden: true })
//     store.dispatch('GenerateRoutes', asyncRouter).then(() => { // 存储路由
//       router.addRoutes(asyncRouter) // 动态添加可访问路由表
//       next({ ...to, replace: true })
//     })
//   })
// }

router.afterEach(() => {
  NProgress.done() // finish progress bar
})

// ================================
// var isRefresh = false
// router.beforeEach(async(to, from, next) => {
//   NProgress.start()
//   document.title = getPageTitle(to.meta.title)
//   var loginId = common.getUrlKey('LoginId')
//   if (loginId) {
//     setLoginId(loginId)
//     NProgress.done()
//   }
//   const hasLgoinId = getLoginId()
//   var access_token = store.state.user.userInfo.AccessToken
//   if (!isRefresh) {
//     if (hasLgoinId) {
//       if (access_token) {
//         // 动态添加菜单
//         addRoutes()
//         isRefresh = true
//         NProgress.done()
//         next({ ...to, replace: true })
//       } else {
//         // 获取用户信息
//         await store.dispatch('user/GetUserInfo', hasLgoinId)
//         // 动态添加菜单
//         addRoutes()
//         isRefresh = true
//         NProgress.done()
//         next({ ...to, replace: true })
//       }
//     } else {
//       window.location.href = `${(window).g.sso}?Realm=${(window).g.Realm}&ReturnUrl=${window.location.href}`
//     }
//   } else {
//     NProgress.done()
//     if (loginId) {
//       var href = document.location.origin + '/#' + to.path.toString()
//       if (to.query) {
//         href += '?'
//       }
//       for (var i in to.query) {
//         href += i.toString()
//         href += '=' + to.query[i]
//       }
//       window.location.href = href
//     }
//     await store.dispatch('user/GetUserInfo', hasLgoinId)
//     next()
//   }
// })


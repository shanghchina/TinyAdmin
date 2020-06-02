import router from './router'
import NProgress from 'nprogress' // progress bar
import 'nprogress/nprogress.css' // progress bar style
import { getLoginId, setLoginId } from '@/utils/auth' // get token from cookie
import getPageTitle from '@/utils/get-page-title'
import store from '@/store'
import common from '@/utils/common'
import { permissionRoutes, constantRoutes } from './router'
NProgress.configure({ showSpinner: false }) // NProgress Configuration

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
// 判断是否存在权限点
export function isExitPermissionPoint(permissionPoint) {
  var permissionPoints = store.state.user.userInfo.PermissionPoints
  return permissionPoints.indexOf(permissionPoint) > -1
}

var isRefresh = false
router.beforeEach(async(to, from, next) => {
  NProgress.start()
  document.title = getPageTitle(to.meta.title)
  var loginId = common.getUrlKey('LoginId')
  if (loginId) {
    setLoginId(loginId)
    NProgress.done()
  }
  const hasLgoinId = getLoginId()
  var access_token = store.state.user.userInfo.AccessToken
  if (!isRefresh) {
    if (hasLgoinId) {
      if (access_token) {
        // 动态添加菜单
        addRoutes()
        isRefresh = true
        NProgress.done()
        next({ ...to, replace: true })
      } else {
        // 获取用户信息
        await store.dispatch('user/GetUserInfo', hasLgoinId)
        // 动态添加菜单
        addRoutes()
        isRefresh = true
        NProgress.done()
        next({ ...to, replace: true })
      }
    } else {
      window.location.href = `${(window).g.sso}?Realm=${(window).g.Realm}&ReturnUrl=${window.location.href}`
    }
  } else {
    NProgress.done()
    if (loginId) {
      var href = document.location.origin + '/#' + to.path.toString()
      if (to.query) {
        href += '?'
      }
      for (var i in to.query) {
        href += i.toString()
        href += '=' + to.query[i]
      }
      window.location.href = href
    }
    await store.dispatch('user/GetUserInfo', hasLgoinId)
    next()
  }
})


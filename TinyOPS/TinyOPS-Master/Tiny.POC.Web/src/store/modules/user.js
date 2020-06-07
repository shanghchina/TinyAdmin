import userApi from '@/api/login'
// import { logout } from '@/api/login'
import { getToken, setToken, removeToken, setLoginId } from '@/utils/auth'

const state = {
  token: getToken(),
  user: {},
  userInfo: JSON.parse(sessionStorage.getItem('userInfo')) || {}
}

const mutations = {
  SET_USERINFO: (state, userInfo) => {
    state.userInfo = userInfo
    sessionStorage.setItem('userInfo', JSON.stringify(userInfo))
  },
  Remove_USERINFO: (state) => {
    state.userInfo = {}
  },
  SET_TOKEN: (state, token) => {
    state.token = token
  }
}

const actions = {
  // 登录
  LoginIn({ commit }, userInfo) {
    const rememberMe = false // userInfo.rememberMe
    return new Promise((resolve, reject) => {
      userApi.loginIn(userInfo.username, userInfo.password, userInfo.code, userInfo.uuid).then(res => {
        setLoginId(userInfo.username)
        setToken(res.Data.token, rememberMe)
        commit('SET_TOKEN', res.Data.token)        
        // console.log(res.Data.token)
        // setUserInfo(res.user, commit)
        // // 第一次加载菜单时用到， 具体见 src 目录下的 permission.js
        // commit('SET_LOAD_MENUS', true)
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },

  Get_SysUser({ commit }, username) {
    return new Promise((resolve, reject) => {
      console.log("1")
      userApi.get_SysUser(username).then(response => {
        console.log("2")
        if (response.Code === '0') {
          console.log("3")
          commit('SET_USERINFO', response.Data)
          resolve(response)
        } else {
          console.log("4")
          // window.location.href = `${(window).g.sso}?Realm=${(window).g.Realm}&ReturnUrl=${window.location.href}`
          // resolve(response)
          resolve(response)
        }
      }).catch(err => {
        console.log("err")
        reject(err)
      })
    })
  },

  RemoveUserInfo({ commit, state }) {
    commit('Remove_USERINFO')
    sessionStorage.removeItem('userInfo')
  },
  // 登出
  LogOut({ commit }) {
    return new Promise((resolve, reject) => {
      userApi.logout().then(res => {
        logOut(commit)
        resolve()
      }).catch(error => {
        logOut(commit)
        reject(error)
      })
    })
  }
}

export const logOut = (commit) => {
  commit('SET_TOKEN', '')
  // commit('SET_ROLES', [])
  removeToken()
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}


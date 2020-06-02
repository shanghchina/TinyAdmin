import userApi from '@/api/user'

const state = {
  userInfo: JSON.parse(sessionStorage.getItem('userInfo')) || {}
}

const mutations = {
  SET_USERINFO: (state, userInfo) => {
    state.userInfo = userInfo
    sessionStorage.setItem('userInfo', JSON.stringify(userInfo))
  },
  Remove_USERINFO: (state) => {
    state.userInfo = {}
  }
}

const actions = {
  GetUserInfo({ commit, state }, loginId) {
    return new Promise((resolve, reject) => {
      userApi.getSsoUserInfo(loginId).then(response => {
        if (response.Code === '0') {
          commit('SET_USERINFO', response.Data)
          resolve(response)
        } else {
          window.location.href = `${(window).g.sso}?Realm=${(window).g.Realm}&ReturnUrl=${window.location.href}`
          resolve(response)
        }
      }).catch(err => {
        reject(err)
      })
    })
  },
  RemoveUserInfo({ commit, state }) {
    commit('Remove_USERINFO')
    sessionStorage.removeItem('userInfo')
  }

}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}


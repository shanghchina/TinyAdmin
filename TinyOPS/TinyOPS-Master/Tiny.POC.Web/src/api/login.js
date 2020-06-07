import request from '@/utils/request'
// 获取Sso单点登陆用户信息
const getSsoUserInfo = (data) => {
  return request({
    url: '/SsoUserInfo/GetSsoUserInfo',
    method: 'post',
    data: { 'LoginId': data }
  })
}

// const loginOut = (data) => {
//   return request({
//     url: '/SsoUserInfo/LoginOut',
//     method: 'post',
//     data: { 'LoginId': data }
//   })
// }

const test = (data) => {
  return request({
    url: '/AppId/TestAdd',
    method: 'get',
    data: ''
  })
}

// 登录校验用户
const loginIn = (username, password, code, uuid) => {
  return request({
    url: 'Login/LoginIn',
    method: 'post',
    data: {
      username,
      password,
      code,
      uuid
    }
  })
}

// 获取用户信息
const get_SysUser = (username) => {
  return request({
    url: 'Login/GetSysUser',
    method: 'post',
    data: username
  })
}

// 退出登录
const logout = () => {
  return request({
    url: 'Login/LoginOut',
    method: 'post'
  })
}

export default {
  getSsoUserInfo,
  logout,
  loginIn,
  get_SysUser,
  test
}

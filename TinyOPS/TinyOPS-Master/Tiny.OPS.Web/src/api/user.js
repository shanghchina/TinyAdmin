import request from '@/utils/request'
// 获取Sso单点登陆用户信息
const getSsoUserInfo = (data) => {
  return request({
    url: '/SsoUserInfo/GetSsoUserInfo',
    method: 'post',
    data: { 'LoginId': data }
  })
}

const loginOut = (data) => {
  return request({
    url: '/SsoUserInfo/LoginOut',
    method: 'post',
    data: { 'LoginId': data }
  })
}

const test = (data) => {
  return request({
    url: '/AppId/TestAdd',
    method: 'get',
    data: ''
  })
}

export default {
  getSsoUserInfo,
  loginOut,
  test

}

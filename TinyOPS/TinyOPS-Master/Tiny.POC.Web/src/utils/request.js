import axios from 'axios'
import common from '@/utils/common'
import store from '@/store'
import md5 from 'js-md5'
import { getToken } from '@/utils/auth'
import { Notification, MessageBox } from 'element-ui'

const service = axios.create({
  baseURL: window.g.api, // url = base url + request url
  timeout: 120000
  // withCredentials: true // send cookies when cross-domain requests
})

// Request interceptors
service.interceptors.request.use(
  (config) => {
    config.headers.AppId = window.g.AppId
    config.headers.Timestamp = common.getRequestDate()
    config.headers.TransactionID = common.getTransactionID()
    config.headers.Version = '1.0'
    var appId = window.g.AppId
    var secrectkey = ''
    var accessToken = ''
    if (store.state.user.userInfo.Name) {
      secrectkey = store.state.user.userInfo.Secrectkey
      accessToken = store.state.user.userInfo.AccessToken
    }
    // 签名，动态令牌加静态令牌 规则: 大写MD5(AppId + Timestamp + SecrectKey)
    var strSign = appId + config.headers.Timestamp + secrectkey
    // 生成签名
    var sign = md5(strSign)
    // console.log(sign)
    config.headers['Token'] = accessToken
    config.headers['Sign'] = sign.toUpperCase()
    if (getToken()) {
      config.headers['Authorization'] = getToken() // 让每个请求携带自定义token 请根据实际情况自行修改
    }
    config.headers['Content-Type'] = 'application/json'
    return config
  },
  (error) => {
    Promise.reject(error)
  }
)

// Response interceptors
service.interceptors.response.use(
  (response) => {
    console.log(res)
    const res = response.data
    const code = res.Code
    if (code) {
      if (code === '0') {
        return res
      } else {
        console.log(res)
        common.toast(res.Message, 'error')
       return Promise.reject('error')
      }
    } else {
      return Promise.reject('error')
    }
  },
  (error) => {
    const code = 0
    if (code) {
      if (code === 401) {
        MessageBox.confirm(
          '登录状态已过期，您可以继续留在该页面，或者重新登录',
          '系统提示',
          {
            confirmButtonText: '重新登录',
            cancelButtonText: '取消',
            type: 'warning'
          }
        ).then(() => {
          store.dispatch('LogOut').then(() => {
            location.reload() // 为了重新实例化vue-router对象 避免bug
          })
        })
      } else {
        const errorMsg = error.message
        if (errorMsg !== undefined) {
          Notification.error({
            title: errorMsg,
            duration: 5000
          })
        }
      }
    } else {
      Notification.error({
        title: '接口请求失败',
        duration: 5000
      })
    }

    // common.toast(error.message, 'error')
    return Promise.reject(error)
  }
)

export default service

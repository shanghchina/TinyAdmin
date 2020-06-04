import axios from 'axios'
import common from '@/utils/common'
import store from '@/store'
import md5 from 'js-md5'

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
    return config
  },
  (error) => {
    // Do something with request error
    console.log(error) // for debug
    Promise.reject(error)
  }
)

// Response interceptors
service.interceptors.response.use(
  (response) => {
    const res = response.data
    if (res.Code && res.Code !== '0') {
      common.toast(res.Message, 'error')
      return res
    } else {
      return res
    }
  },
  (error) => {
    common.toast(error.message, 'error')
    return Promise.reject(error)
  }
)

export default service

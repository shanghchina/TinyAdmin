//// 基于axios基础配置上的封装库
//import requestbase from '@/utils/request'

//// TODO:不再单独传递Token，统一token入口在拦截器里
//export function send(method = 'post', uri, data = null, params = null, token = null) {
//  return new Promise((resolve, reject) => {
//    try {
//      requestbase({
//        method: method,
//        url: uri,
//        params: params,
//        data: data
//      }).then(res => {
//        resolve(res)
//      }).catch(error => {
//        console.error(error)
//      })
//    } catch (error) {
//      reject(error)
//      console.log(error)
//    }
//  })
//}

//// 通用请求
//export function request(operation) {
//  return requestbase(operation)
//}



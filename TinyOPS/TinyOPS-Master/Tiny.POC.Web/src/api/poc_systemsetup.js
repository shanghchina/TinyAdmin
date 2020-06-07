import request from '@/utils/request'
// 系统设置-后台维护


export function GetBasicData(data) {
  return request({
    url: '/JobData/GetBasicData',
    method: 'Post',
    data
  })
}

export function GetThresholdData(data) {
  return request({
    url: '/JobData/GetThresholdData',
    method: 'Post',
    data
  })
}

export function GetProductData(data) {
  return request({
    url: '/JobData/GetProductData',
    method: 'Post',
    data
  })
}

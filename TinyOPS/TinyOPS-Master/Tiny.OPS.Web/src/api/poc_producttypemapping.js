import request from '@/utils/request'
// 系统设置-产品分类映射

export function GetPageInfoList(data) {
  return request({
    url: '/ProductType/GetPageInfoList',
    method: 'Post',
    data
  })
}

export function GetProductTypeLevel(data) {
  return request({
    url: '/ProductType/GetProductTypelevel',
    method: 'Post',
    data
  })
}

export function AddOrUpdateProductTypeMap(data) {
  return request({
    url: '/ProductType/AddOrUpdateProductTypeMap',
    method: 'Post',
    data
  })
}

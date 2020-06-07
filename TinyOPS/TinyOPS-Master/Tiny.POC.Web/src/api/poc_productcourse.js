//import { send } from './apitools'

// 产品管理-产品管理
//export function GetListBy(params) {
//  return send('post', '/StudentManage/GetListByOrgIdWithFromSystem', params)
//}
import request from '@/utils/request'

//获取产品分类下拉列表树状结构
export function getProductTypeLevel(data) {
  return request({
    url: '/ProductType/GetProductTypelevel',
    method: 'Post',
    data
  })
}

// 获取产品管理列表
export function getPOCProductByPageList(data) {
  return request({
    url: '/POCProduct/GetPOCProductByPage',
    method: 'Post',
    data
  })
}
//获取提取产品
export function getExtractedCourseByPageList(data) {
  return request({
    url: '/POCProduct/GetEXTCourseByPage',
    method: 'Post',
    data
  })
}

//获取关联产品
export function getAllocatedCourseByPageList(data) {
  return request({
    url: '/POCProduct/GetEXTCourseByPage',
    method: 'Post',
    data
  })
}

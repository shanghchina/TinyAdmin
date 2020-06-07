import request from '@/utils/request'
// 产品管理-产品分类管理

const getProductTypeList = (data) => {
  return request({
    url: '/ProductType/GetProductTypeList',
    method: 'post',
    data: data
  })
}

const operateProductType = (data) => {
  return request({
    url: '/ProductType/OperateProductType',
    method: 'post',
    data: data
  })
}

const operateSwitch = (data) => {
  return request({
    url: '/ProductType/OperateSwitch',
    method: 'post',
    data: data
  })
}

const deleteProductType = (data) => {
  return request({
    url: '/ProductType/DeleteProductType',
    method: 'post',
    data: data
  })
}

const getProductTypeMappingList = (data) => {
  return request({
    url: '/ProductType/GetProductTypeMappingList',
    method: 'post',
    data: data
  })
}
export default {
  getProductTypeList,
  operateProductType,
  deleteProductType,
  operateSwitch,
  getProductTypeMappingList
}

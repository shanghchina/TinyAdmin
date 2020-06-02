import request from '@/utils/request'
// 系统设置-基础参数设置

// 获取POC系统维护-基础参数大类列表
export function getPOCBasiceParamsList(data) {
  return request({
    url: '/Dictionary/GetPOCBasicParamsParentList',
    method: 'Post',
    data
  })
}

// 获取POC系统维护-基础参数子节点列表
export function getPOCBasiceChildParamsList(data) {
  return request({
    url: '/Dictionary/GetPOCBasiceChildParamsList',
    method: 'Post',
    data
  })
}

// 系统维护-基础参数子节点-提交保存
export function submitChildParamsOption(data) {
  return request({
    url: '/Dictionary/SubmitChildParamsOption',
    method: 'Post',
    data
  })
}

// 系统维护-基础参数子节点-删除子节点判断是否已经映射
export function checkHasMappedBasciData(data) {
  return request({
    url: '/Dictionary/CheckHasMappedBasciData',
    method: 'Post',
    data
  })
}

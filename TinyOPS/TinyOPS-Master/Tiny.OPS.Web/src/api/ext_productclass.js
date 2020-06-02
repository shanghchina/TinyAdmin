import request from '@/utils/request'
// 事业部产品池-班级管理

// 获取班级管理列表
export function getEXTClassByPageList(data) {
  return request({
    url: '/EXTClass/GetEXTClassByPage',
    method: 'Post',
    data
  })
}

// 导出excle
export function exportExcle(data) {
  const config = {
    responseType: 'blob'
  }
  return request['post']('/EXTClass/ExportExcle', data, config)
}

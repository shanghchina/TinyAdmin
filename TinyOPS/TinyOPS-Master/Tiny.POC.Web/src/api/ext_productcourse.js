import request from '@/utils/request'
// 事业部产品池-课程管理

export function GetProductCoursePageInfo(data) {
  return request({
    url: '/ExtProductCourse/GetProductCoursePageInfo',
    method: 'Post',
    data
  })
}

// 导出excle
export function exportExcle(data) {
  const config = {
    responseType: 'blob'
  }
  return request['post']('/ExtProductCourse/ExportExcle', data, config)
}

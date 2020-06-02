import request from '@/utils/request'
// 事业部产品池-基础参数

export function GetBasicDataInfoPage(data) {
  return request({
    url: '/BasicData/GetBasicDataInfoPage',
    method: 'Post',
    data
  })
}

export function exportExcle(data) {
  const config = {
    responseType: 'blob'
  }
  return request['post']('/BasicData/ExportExcle', data, config)
}


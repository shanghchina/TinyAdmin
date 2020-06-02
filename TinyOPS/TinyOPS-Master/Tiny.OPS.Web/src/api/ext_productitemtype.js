import request from '@/utils/request'
// 事业部产品池-产品分类
const getProductItemTypeList = (data) => {
  return request({
    url: '/ExtProductType/GetProductItemTypeList',
    method: 'post',
    data: data
  })
}

// 导出excle
const exportExcel = (data) => {
  const config = {
    responseType: 'blob'
  }
  return request['post']('/ExtProductType/ExportExcel', data, config)
}

export default {
  getProductItemTypeList,
  exportExcel
}

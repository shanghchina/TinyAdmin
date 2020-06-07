import request from '@/utils/request'

// 事业部产品池-提取器

// 获取字典数据
const getDict = (data) => {
  return request({
    url: '/Dictionary/GetDictionary',
    method: 'post',
    data: data
  })
}

// 获取提取器列表
const getExtractorList = (data) => {
  return request({
    url: '/POCExtractor/GetExtractorDetaiList',
    method: 'post',
    data: data
  })
}
// 导出excle
const exportExcle = (data) => {
  const config = {
    responseType: 'blob'
  }
  return request['post']('/POCExtractor/ExportExcle', data, config)
}

// 提取器预览
const previewJudge = (data) => {
  return request({
    url: '/POCExtractor/PreviewJudge',
    method: 'post',
    data: data
  })
}
// 创建提取器
const createExtractor = (data) => {
  return request({
    url: '/POCExtractor/createExtractor',
    method: 'post',
    data: data
  })
}

// 取消提取器
const cancleExtractor = (data) => {
  return request({
    url: '/POCExtractor/cancleExtractor',
    method: 'post',
    data: data
  })
}

// 获取提取器信息
const getExtractorInfo = (data) => {
  return request({
    url: '/POCExtractor/GetExtractorInfo',
    method: 'post',
    data: data
  })
}

// 获取基础信息
const getBaseData = (data) => {
  return request({
    url: '/BasicData/GetBasicDataByDictIdMap',
    method: 'post',
    data: data
  })
}

// 获取课程信息
const getCouseListByExtractor = (data) => {
  return request({
    url: '/POCExtractor/GetCouseListByExtractor',
    method: 'post',
    data: data
  })
}

// 提取产品
const extractProducts = (data) => {
  return request({
    url: '/POCExtractor/ExtractProducts',
    method: 'post',
    data: data
  })
}
// 判断来源系统所有的产品类型是否映射
const productTypeIsAllMapping = (data) => {
  return request({
    url: '/ExtProductType/isAllMapping',
    method: 'post',
    data: data
  })
}

// 判断来源系统所有的基础参数是否映射
const baseDataIsAllMapping = (data) => {
  return request({
    url: '/BasicData/isAllMapping',
    method: 'post',
    data: data
  })
}

export default {
  getDict,
  getExtractorList,
  exportExcle,
  previewJudge,
  createExtractor,
  cancleExtractor,
  getExtractorInfo,
  getBaseData,
  getCouseListByExtractor,
  extractProducts,
  productTypeIsAllMapping,
  baseDataIsAllMapping
}

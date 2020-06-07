import request from '@/utils/request'
// 系统设置-基础参数映射

const getBasicDataMapList = (data) => {
  return request({
    url: '/BasicData/GetBasicDataMapList',
    method: 'post',
    data: data
  })
}

const operateBasicDataMap = (data) => {
  return request({
    url: '/BasicData/OperateBasicDataMap',
    method: 'post',
    data: data
  })
}

export default {
  getBasicDataMapList,
  operateBasicDataMap
}

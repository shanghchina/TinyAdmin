module.exports = {

  title: '项目中心',

  /**
   * @type {boolean} true | false
   * @description 固定头部
   */
  fixedHeader: false,

  /**
   * @type {boolean} true | false
   * @description 是否显示logo
   */
  sidebarLogo: false,

  /**
   * @description 是否显示 tagsView
   */
  tagsView: true,
  /**
   * @description 记住密码状态下的token在Cookie中存储的天数，默认1天
   */
  tokenCookieExpires: 1,
  /**
   * @description 记住密码状态下的密码在Cookie中存储的天数，默认1天s
   */
  passCookieExpires: 1,
  /**
   * @description 是否只保持一个子菜单的展开
   */
  uniqueOpened: true,
  /**
   * @description token key
   */
  TokenKey: 'EL-ADMIN-TOEKN',
  /**
   * @description 请求超时时间，毫秒（默认2分钟）
   */
  timeout: 1200000,
  /**
   * 是否显示设置的底部信息
   */
  showFooter: true,
  /**
   * 底部文字，支持html语法
   */
  footerTxt: '© 2020 <a href="http://www.baidu.org" target="_blank">Apache License 2.0</a>',
  /**
   * 备案号
   */
  caseNumber: 'ICP备'
}

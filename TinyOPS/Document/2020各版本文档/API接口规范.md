<center> API接口规范</center>

 

| **起草人** |  | **签发人** |  |
| ---------- | ------ | ---------- | ------ |
|            |        |            |        |




#### **修改历史**

| 日期      | 作者   | 版本 | 修改内容及原因         |
| --------- | ------ | ---- | ---------------------- |
| 2016/4/15 |  | V1.0 |                        |
| 2019/5/20 |  | V2.0 | 明确及完善标准，并实施 |
|           |        |      |                        |





## 一  、目录地址规范

适用于所有系统

### 1.1. 协议

API与用户的通信协议，目前统一使用HTTP/HTTPS协议，确保交互数据的传输安全。

 

### 1.2. 域名

Ø  应该尽量将API部署在专用域名之下。

Ø  **域名规范**：http://api.app.domaintype.example.com

Øapp为应用名称，domaintype为统一应用域

 

### 1.3. 路径规则

Ø  **网关路由**：http://api. example.com/domaintype/模块名称/操作方法

Ø  **应用路由**：http://api.app.domaintype.example.com/模块名称/操作方法

 

举例来说，权限系统获用户信息模块获取用户，则它的路径应该设计成下面这样。

http://api.rls.dev.com/Users/Get_UserList

 

 

## 二  、接口认证规范

*安全认证方式需要可配置供业务系统配置，需要在开发方案中明确。*

### 2.1、基于IP白名单认证

Ø  适用于：服务端对服务端通信

Ø  白名单+黑名单验证IP,不满足条件直接返回异常，减少压力

### 2.2、基于秘钥签名认证

Ø  适用所有应用=>应用范围。

Ø  所有应用请求及反馈需签名，并验证。

Ø  **签名规则**：大写MD5(AppId+Timestamp+Json+SecrectKey)

由服务端统一颁发APPID与SecretKey给客户端，客户端每次请求接口需要根据签名规则进行签名，并将签名放入Http请求Header中，对于客户端要求妥善保管私有的ID与秘钥，建议加密此类信息再存储；防止数据被篡改。

响应总时间=2次加密时间+响应时间。

Ø需人工干预刷新 SecertKey，周期1季度。

Ø请求/响应内容安全（可选）：根据固定的（需人工干预刷新，周期1季度）加密密钥对请求/响应用的内容加解密。



### 2.3、基于安全令牌认证

Ø  适用于多用户=>应用级别

Ø  采用安全令牌Token+签名认证，模式API登录获取安全令牌，拿安全令牌访问对应接口，方便接口认证和鉴权，Token发送时放入Header中；

Ø流程表示为：

**登录>返回安全令牌=>客户端存储令牌=>使用令牌调用其它接口**

服务端作为特殊的用户，登录后返回Token、加密秘钥与IV(可选配置)等信息

用户登录直接增加返回安全令牌，应用使用用户的令牌访问接口

Ø  请求/响应内容安全（可选）：根据安全令牌同步动态的加密密钥对请求/响应用的内容加解密。

 

## 三、 接口文档规范

 

### 3.1、自动化文档统一引用Swagger

Ø  源代码层面统一引用Swagger

Ø  提供统一路由给用户、注意接口权限配置

Ø  自动文档UI统一地址：http://api.app.domaintype.example.com/swagger

Ø  文档中应用包括明确的接口应用场景描述、相对地址、请求类型、编码类型、请求参数说明及示例、反馈内容及示例、状态说明。
Ø 文档检查将做为研发计划的的一个工作项，由项目技术负责人检查。




##  四、数据传输规范

### 4.1、数据传输格式

非特殊接口，参数与返回结果统一使用： 

Ø  Json作为传输数据方式

Ø  Post作为主要调用接口方式

Ø  编码方式统一使用Utf-8

 

### 4.2、数据结构

规范数据结果集；接口返回的结果内容除了要返回业务数据外,还得返回处理状态，数据格式统一规范定义为:

**数据头(描述数据信息)**

**------------------------**

**数据体(业务数据)**



 

#### 4.2.1 请求/返回接口HTTP Header

公共头部：所有调用API接口需按如下标准添加头部消息。

| 参数名称      | 必选 | 类型   | 说明                | 备注                                                         |
| ------------- | ---- | ------ | ------------------- | ------------------------------------------------------------ |
| AppId         | 是   | String | 后台统一分配的APPID |                                                              |
| Token         | 是   | String | 动态令牌            | 使用自有的appid和秘钥通过令牌接口获取                        |
| Timestamp     | 是   | String | 时间戳              | 发送请求的时间，格式"yyyy-MM-dd   HH:mm:ss"   例如：2014-07-24   03:07:50 |
| TransactionID | 是   | String | 消息流水号          | 消息流水号。yyyyMMddHHmmss+4位顺序号                         |
| Sign          | 是   | String | 签名，              | 规则:大写MD5(AppId+Timestamp+TransactionID+Json+SecrectKey)  |
| Version       | 是   | String | 版本号              | 示例：1.0                                                    |

 

#### 4.2.2 请求接口数据结构

Ø 整体传输仍然使用json格式，直接接口请求实体 。
Ø 如对APPID配置了信息加密，请求消息体为(Json)加密。

#### 4.2.3接口返回数据结构

整体传输仍然使用json格式，包含下面必选：

| 参数名称 | 必选 | 类型   | 说明                   | 备注                                      |
| -------- | ---- | ------ | ---------------------- | ----------------------------------------- |
| Code     | 是   | String | 接口是否调用成功状态码 | 0成功，-1未知错误，其他状态码根据系统定义 |
| Message  | 是   | String | 失败消息/成功附加消息  |                                           |
| Data     | 是   | String | 具体的业务数据         | 统一json格式                              |

 示例数据：

```json
{
    "Code": "0",
    "Message": "OK",
    "Data": {}
}
```

 

 

### 4.3、跨域访问

4.3.1、同域跳转方式

  跨域本质是浏览器安全机制的拦截，所以可以以本系统做跳板，apiurl、请求json作为参数，在本系统内获取数据后返回

 

4.3.2、基于Access-Control-Allow-Origin原理

  .Net应用统一使用Microsoft.AspNet.WebApi.Cors包。

 

## 五、接口日志规范

### 5.1、本地文本日志输出

Ø  统一使用Log4Net记录

Ø  记录内容包括：时间、应用域、应用名称、模块名称、请求/应答头（包括了身份信息）、对象、方法、日志级别类型、日志内容（日志内容需统一格式）

示例日志内容：

```json
{
    "TransactionID": "201905301419010001",
    "LogTime": "yyyy-MM-dd HH:mm:ss",
    "DomainTypeId": 1000,
    "ApplicationName": "",
    "Header": {
        "AppId": "",
        "Token": "",
        "Timestamp": "",
        "Sign": "",
        "Version": ""
    },
    "Ojbect": "日志记录所在命名空间明细",
    "Method ": "日志记录所在方法",
    "Loglevel": "Fatal/Error/Warn/Info/Debug",
    "IP": "127.0.0.1",
    "URI": "",
    "LogData": ""
}
```



示例日志内容字段说明:

| 参数名称        | 类型     | 说明                                                    | 备注 |
| --------------- | -------- | ------------------------------------------------------- | ---- |
| TransactionID   | String   | 消息流水号。yyyyMMddHHmmss+4位顺序号                    |      |
| LogTime         | datetime | yyyy-MM-dd HH:mm:ss                                     |      |
| DomainTypeId    | int      | 统一应用域ID                                            |      |
| ApplicationName | String   | 应用名称(.net解决方案中的项目名称)                      |      |
| Ojbect          | String   | 日志记录所在命名空间明细                                |      |
| Method          | String   | 日志记录所在方法                                        |      |
| Loglevel        | int      | 1000.Fatal  2000.Error  3000.Warn 4000.Info  5000.Debug |      |
| IP              | String   | 请求用户IP                                              |      |
| URI             | String   | 请求地址                                                |      |
| LogData         | String   | 日志内容                                                |      |

Ø  至少要在接口方法入口Info记录请求内容，如非 json 内容则转为 json 记录；

Ø  至少要在接口方法返回结果前Info记录反馈内容；

Ø  至少要在接口方法中捕获全局异常

### 5.2、Kafka消息输出

Ø  基于Log4net上，将日志消息输出到Kafka队列上。

Ø  基于ELK建立日志系统。

## 六、接口活跃检查规范

### 6.1、活跃接口相对地址
http://api.app.domaintype.example.com/lively/test

### 6.2、活跃接口功能定义

Ø 能够正确返回结果

Ø 能够正确读写文件

Ø 能够正确读写数据库

Ø使用服务端验证

返回结果说明：

| 参数名称 | 必选 | 类型   | 说明                   | 备注                                      |
| -------- | ---- | ------ | ---------------------- | ----------------------------------------- |
| Code     | 是   | String | 接口是否调用成功状态码 | 0成功，-1未知错误，其他状态码根据系统定义 |
| Message  | 是   | String | 失败消息/成功附加消息  |                                           |
| Data     | 是   | String | 具体的业务数据         | 统一json格式                              |
示例返回数据：
```json
{
    "Code": "0",
    "Message": "OK",
    "Data": {}
}
```

## 七、统一状态码

应用系统可维护自己的状态码，统一状态码则为：DomainTypeId_应用系统状态码。

| 状态码 | 说明     |
| ------ | -------- |
| 0      | 成功     |
| -1     | 未知错误 |
|        |          |
|        |          |
|        |          |
|        |          |
|        |          |
|        |          |
|        |          |


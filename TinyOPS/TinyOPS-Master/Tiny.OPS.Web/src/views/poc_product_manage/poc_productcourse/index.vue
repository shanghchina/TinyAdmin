<!--产品管理-产品管理-->
<template>
  <div class="dashboard-container">
    <el-col :span="24">
      <el-form :inline="true" :model="formOptions" class="productCourseBox">
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6 ">
          <el-form-item label="产品分类">
            <!--<el-select v-model="formOptions.FormSystem" placeholder="请选择" multiple collapse-tags>-->
            <!--<el-option label="校管家-少儿" value="01"></el-option>-->
            <!--<el-option label="boss-少儿" value="02"></el-option>-->
            <!--</el-select>-->
            <el-cascader
              ref="productTypeTree"
              :options="formOptions.options"
              :props="formOptions.props"
              collapse-tags
              clearable
            />
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6 ">
          <el-form-item label="产品状态">
            <el-select v-model="formOptions.QueryCourseStatus" placeholder="请选择">
              <el-option label="全部" value="2" />
              <el-option label="有效" value="1" />
              <el-option label="无效" value="0" />
            </el-select>
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6 ">
          <el-form-item label="产品名称">
            <el-input v-model="formOptions.QueryCourseName" />
          </el-form-item>
        </div>
        <div class="el-col el-col-sm-12 el-col-md-6 el-col-lg-6 marginTop">
          <el-form-item>
            <el-button v-if="isExitPermissionPoint('POC_ProductCourse_Query')" type="primary" @click="onDoQuery">查询</el-button>
          </el-form-item>
        </div>
      </el-form>
      <el-table :data="productTable.tableData" border style="width: 100%" empty-text=" ">
        <el-table-column prop="ProductNo" label="产品编号"  width="120"/>
        <el-table-column prop="CourseName" label="产品名称" width="150" />
        <el-table-column prop="ProductTypeLable" label="产品分类" width="300" />
        <el-table-column prop="CourseStatus" label="产品状态" width="120">
          <template slot-scope="scope">
            <span>{{ scope.row.CourseStatus=='1'? '有效':'无效' }}</span>
          </template>
        </el-table-column>
        <el-table-column prop="CampusCountName" label="授权校区(个)" width="120"/>
        <el-table-column prop="proPoolLink" label="产品池关联" width="150">
          <template slot-scope="scope">
            <el-button type="primary" size="small" @click="handleClick(scope.row)">查看</el-button>
          </template>
        </el-table-column>
        <el-table-column prop="FeeUnitPrice" label="产品单价" />
        <el-table-column prop="FeeUnitPriceName" label="产品单位" />
        <el-table-column prop="TotalClassHour" label="课时数" />
        <el-table-column prop="CourseYear" label="所属年份" />
        <el-table-column prop="GradeDictName" label="所属年级" />
        <el-table-column prop="CategoryDictName" label="所属类型" width="150" />
        <el-table-column prop="SubjectDictName" label="所属科目" />
        <el-table-column prop="TermDictName" label="所属期段" />
        <el-table-column prop="ClassTypeDictName" label="所属班型" />
        <el-table-column prop="ProductCreatedDate" label="创建日期" width="200">
          <template slot-scope="scope">
            <span>{{ scope.row.ProductCreatedDate |formatDate('yyyy-MM-dd hh:mm:ss') }}</span>
          </template>
        </el-table-column>
        <el-table-column prop="ProductUpdateDate" label="更新日期" width="200">
          <template slot-scope="scope">
            <span>{{ scope.row.ProductUpdateDate |formatDate('yyyy-MM-dd hh:mm:ss') }}</span>
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        :current-page.sync="currentPage"
        :page-sizes="[10, 20, 30, 40]"
        :page-size="productTable.pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="productTable.pageTotal"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
      />

    </el-col>
    <div class="dialogTable">
      <el-dialog title="产品池关联" :visible.sync="dialogTable.dialogTableVisible" width="70%">
        <div style="margin-bottom: 20px">
          <p class="p_title">提取产品</p>
          <el-table :data="dialogTable.extractedProductTable.tableData" border>
            <el-table-column property="POCSource" label="系统来源" width="150">
              <template slot-scope="scope">
                <span>{{ scope.row.POCSource =='100'? '校管家': '' }}</span>
              </template>
            </el-table-column>
            <el-table-column property="TeachLevelOneOrgName" label="所属事业部" width="200" />
            <el-table-column property="CourseName" label="课程名称" width="300" />
            <el-table-column property="ProductTypeName" label="所属分类" />
            <el-table-column property="CourseStatus" label="课程状态">
              <template slot-scope="scope">
                <span>{{ scope.row.CourseStatus=='1'? '有效':'无效' }}</span>
              </template>
            </el-table-column>
            <el-table-column property="FeeUnitPrice" label="课时单价" />
            <el-table-column property="FeeUnitPriceName" label="课时单位" />
            <el-table-column property="TotalClassHour" label="课时数" />
            <el-table-column property="CourseYear" label="所属年份" />
            <el-table-column property="GradeName" label="所属年级" />
            <el-table-column property="CategoryName" label="所属类型" />
            <el-table-column property="SubjectName" label="所属科目" />
            <el-table-column property="TermName" label="所属期段" />
            <el-table-column property="ClassTypeName" label="所属班型" />
            <el-table-column property="CampusCountName" label="授权校区(个)" width="120" />
            <el-table-column prop="ProductCreatedDate" label="创建日期" width="200">
              <template slot-scope="scope">
                <span>{{ scope.row.ProductCreatedDate |formatDate('yyyy-MM-dd hh:mm:ss') }}</span>
              </template>
            </el-table-column>
            <el-table-column prop="ProductUpdateDate" label="更新日期" width="200">
              <template slot-scope="scope">
                <span>{{ scope.row.ProductUpdateDate |formatDate('yyyy-MM-dd hh:mm:ss') }}</span>
              </template>
            </el-table-column>
          </el-table>
        </div>
        <div>
          <p class="p_title">关联产品</p>
          <el-table :data="dialogTable.allocatedProductTable.tableData" border>
            <el-table-column property="POCSource" label="系统来源" width="150">
              <template slot-scope="scope">
                <span>{{ scope.row.POCSource =='100'? '校管家': '' }}</span>
              </template>
            </el-table-column>
            <el-table-column property="TeachLevelOneOrgName" label="所属事业部" width="200" />
            <el-table-column property="CourseName" label="课程名称" width="300" />
            <el-table-column property="ProductTypeName" label="所属分类" />
            <el-table-column property="CourseStatus" label="课程状态">
              <template slot-scope="scope">
                <span>{{ scope.row.CourseStatus=='1'? '有效':'无效' }}</span>
              </template>
            </el-table-column>
            <el-table-column property="FeeUnitPrice" label="课时单价" />
            <el-table-column property="FeeUnitPriceName" label="课时单位" />
            <el-table-column property="TotalClassHour" label="课时数" />
            <el-table-column property="CourseYear" label="所属年份" />
            <el-table-column property="GradeName" label="所属年级" />
            <el-table-column property="CategoryName" label="所属类型" />
            <el-table-column property="SubjectName" label="所属科目" />
            <el-table-column property="TermName" label="所属期段" />
            <el-table-column property="ClassTypeName" label="所属班型" />
            <el-table-column property="CampusCountName" label="授权校区(个)" width="120"/>
            <el-table-column prop="ProductCreatedDate" label="创建日期" width="200">
              <template slot-scope="scope">
                <span>{{ scope.row.ProductCreatedDate |formatDate('yyyy-MM-dd hh:mm:ss') }}</span>
              </template>
            </el-table-column>
            <el-table-column prop="ProductUpdateDate" label="更新日期" width="200">
              <template slot-scope="scope">
                <span>{{ scope.row.ProductUpdateDate |formatDate('yyyy-MM-dd hh:mm:ss') }}</span>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination
            :current-page.sync="dialogTable.allocatedProductTable.currentPage"
            :page-sizes="[10, 20, 30, 40]"
            :page-size="dialogTable.allocatedProductTable.pageSize"
            layout="total, sizes, prev, pager, next, jumper"
            :total="dialogTable.allocatedProductTable.pageTotal"
            @size-change="handleDialogTableSizeChange"
            @current-change="handleDialogTableCurrentChange"
          />
        </div>
        <div slot="footer" class="dialog-footer" style="text-align: center">
          <el-button type="primary" @click="dialogTable.dialogTableVisible = false">关闭</el-button>
        </div>
      </el-dialog>
    </div>
  </div>
</template>

<script>
import * as api from '../../../api/poc_productcourse'
import * as Loading from '@/utils/loading'
export default {
  data() {
    return {
      formOptions: {
        FormSystem: '', // 系统来源
        QueryCourseStatus: '2', // 所属事业部
        State: '', // 产品分类状态
        QueryCourseName: '', // 产品类名称
        props: { multiple: true },
        options: [{}]
      },
      // 当前页面表格
      productTable: {
        tableData: [{}],
        currentPage: 1,
        pageTotal: 0,
        pageSizes: [10, 20, 30, 40],
        pageSize: 10
      },
      currentPage: 1,
      dialogTable: {
        dialogTableVisible: false,
        // 提取产品
        extractedProductTable: {
          tableData: [{}],
          currentPage: 1,
          pageTotal: 0,
          // pageSizes: [10, 20, 30, 40],
          pageSize: 1000
        },
        // 关联产品
        allocatedProductTable: {
          tableData: [{}],
          currentPage: 1,
          pageTotal: 0,
          pageSizes: [10, 20, 30, 40],
          pageSize: 10
        },
        // 提取产品请求信息
        requsetExtractPageData: {
          FKProductCourseGuid: '00000000-0000-0000-0000-000000000000', // FK产品信息表guid
          ExtractStatus: 2000, // 提取状态
          PageSize: 1000,
          PageIndex: 1
        },
        // 关联产品请求信息
        requsetAssociatedPageData: {
          FKProductCourseGuid: '00000000-0000-0000-0000-000000000000', // FK产品信息表guid
          ExtractStatus: 3000, // 提取状态
          PageSize: 10,
          PageIndex: 1
        }
      },
      requsetWhereData: {
        ProductTypeGuid: '00000000-0000-0000-0000-000000000000',
        CourseStatus: '2', // 默认全部
        CourseName: '',
        PageSize: 10,
        PageIndex: 1,
        CheckedNodeGuids: [],
        selectNodeGuids: []
      },
      tableRowGuid: '00000000-0000-0000-0000-000000000000', // 记录行的产品ProductGuid
      CheckedTreeNodes: {}
    }
  },
  created() {
    this.productTable.pageSize = 10
    this.productTable.currentPage = 1
    this.getPOCProductByPageList(1)
    this.getProductTypeLevel()
  },
  methods: {
    onDoQuery() {
      console.log('onDoQuery!')
      this.productTable.currentPage = 1
      this.getPOCProductByPageList(1)
      console.log('onDoQuery successed!')
    },
    // 查看事件
    handleClick(row) {
      this.dialogTable.dialogTableVisible = true
      console.log(row)
      this.tableRowGuid = row.ProductGuid
      console.log(this.tableRowGuid)
      console.log('正在获取提取产品')
      this.getExtractedCourseByPageList(row)
      console.log('正在获取关联产品')
      this.dialogTable.requsetAssociatedPageData.pageSize = 10
      this.getAllocatedCourseByPageList(this.tableRowGuid, 1)
    },
    handleSizeChange(val) {
      // 改变每页显示的条数
      this.productTable.pageSize = val
      this.requsetWhereData.pageSize = val
      console.log(`每页 ${val} 条`)
      this.getPOCProductByPageList(1)
      // 改变每页显示的条数时，要将页码显示到第一页
    },
    handleCurrentChange(val) {
      console.log(`当前页: ${val}`)
      this.productTable.currentPage = val
      var ps = this.productTable.pageSize
      if (typeof (ps) === 'undefined') {
        this.productTable.pageSize = 10
      }
      this.getPOCProductByPageList(val)
      console.log(`当前页: ${val}`)
    },
    // 查看界面分页
    handleDialogTableSizeChange(val) {
      console.log(`每页 ${val} 条`)
      debugger
      // // 改变每页显示的条数
      this.dialogTable.allocatedProductTable.pageSize = val
      this.dialogTable.requsetAssociatedPageData.pageSize = val
      // 改变每页显示的条数时，要将页码显示到第一页
      this.getAllocatedCourseByPageList(this.tableRowGuid, 1)
      console.log(`当前页: ${val}`)
    },
    handleDialogTableCurrentChange(val) {
      console.log(`每页 ${val} 条`)

      // this.dialogTable.allocatedProductTable.currentPage = val
      var ps = this.dialogTable.allocatedProductTable.pageSize
      if (typeof (ps) === 'undefined') {
        this.dialogTable.allocatedProductTable.pageSize = 10
      }
      this.getAllocatedCourseByPageList(this.tableRowGuid, this.dialogTable.allocatedProductTable.currentPage)
      console.log(`当前页: ${val}`)
    },
    // 获取产品分类树
    async getProductTypeLevel() {
      console.log('begin do getProductTypeLevel......')
      try {
        const data = await api.getProductTypeLevel()
        // debugger
        if (
          data.Code.toString() === '0'
        ) {
          console.log('getPOCDictParams success')
          this.formOptions.options = data.Data
        } else {
          console.log('获取产品分类无数据')
          this.common.toast('获取产品分类无数据')
        }
      } catch (e) {
        console.log(e)
      }
    },
    // 产品列表
    async getPOCProductByPageList(currentPageIndex) {
      console.log('begin do getPOCProductByPageList......')
      Loading.showLoading()
      try {
        console.log(this.formOptions.QueryCourseName)
        console.log(this.formOptions.QueryCourseStatus)

        // debugger
        if ((this.$refs.productTypeTree === null) || (this.$refs.productTypeTree === undefined)) {
          this.CheckedTreeNodes = {}
        } else {
          this.CheckedTreeNodes = this.$refs.productTypeTree.getCheckedNodes()
        }
        console.log(this.CheckedTreeNodes)

        this.requsetWhereData.selectNodeGuids.forEach((item, index, arr) => {
          arr.splice(index, 1)
        })
        for (let i = 0; i < this.CheckedTreeNodes.length; i++) {
          console.log(this.CheckedTreeNodes[i].label)
          const strGuid = this.CheckedTreeNodes[i].value
          if (strGuid !== null && strGuid !== 'undefined' && strGuid !== '') {
            if (strGuid.length === 36) {
              this.requsetWhereData.selectNodeGuids.push(strGuid)
            }
            console.log(strGuid)
          }
        }

        this.requsetWhereData.CheckedNodeGuids = this.requsetWhereData.selectNodeGuids

        this.requsetWhereData.CourseName = this.formOptions.QueryCourseName
        this.requsetWhereData.CourseStatus = this.formOptions.QueryCourseStatus
        this.requsetWhereData.PageIndex = currentPageIndex
        console.log(this.requsetWhereData)
        const data = await api.getPOCProductByPageList(this.requsetWhereData)
        // debugger
        if (
          data.Code.toString() === '0'
        ) {
          console.log('getPOCProductByPageList success')
          this.productTable.tableData = data.Data.ReusltList
          this.productTable.pageTotal = data.Data.RecordCount
        } else {
          console.log('无数据')
          this.common.toast('无数据')
        }
      } catch (e) {
        // this.$indicator.close()
        console.log(e)
      }
      Loading.hideLoading()
    },
    // 提取产品
    async getExtractedCourseByPageList(row) {
      console.log('begin do getExtractedCourseByPageList......')
      Loading.showLoading()
      try {
        this.dialogTable.requsetExtractPageData.FKProductCourseGuid = row.ProductGuid
        this.dialogTable.requsetExtractPageData.ExtractStatus = 2000
        console.log(row.ProductGuid)
        this.dialogTable.requsetExtractPageData.pageSize = 1000
        // debugger
        const data = await api.getExtractedCourseByPageList(this.dialogTable.requsetExtractPageData)
        // debugger
        if (
          data.Code.toString() === '0'
        ) {
          console.log('getExtractedCourseByPageList success')
          this.dialogTable.extractedProductTable.tableData = data.Data.ReusltList
          this.dialogTable.extractedProductTable.pageTotal = data.Data.RecordCount
        } else {
          console.log('提取产品无数据')
          this.common.toast('提取产品无数据')
        }
      } catch (e) {
        console.log(e)
      }
      Loading.hideLoading()
    },
    // 关联产品
    async getAllocatedCourseByPageList(productGuid, currentPageIndex) {
      console.log('begin do getAllocatedCourseByPageList......')
      Loading.showLoading()
      try {
        console.log(this.dialogTable.requsetAssociatedPageData)
        this.dialogTable.requsetAssociatedPageData.FKProductCourseGuid = productGuid
        this.dialogTable.requsetAssociatedPageData.ExtractStatus = 3000
        console.log(productGuid)
        this.dialogTable.requsetAssociatedPageData.PageIndex = currentPageIndex
        this.dialogTable.requsetAssociatedPageData.PageSize = this.dialogTable.allocatedProductTable.pageSize
        debugger
        const data = await api.getAllocatedCourseByPageList(this.dialogTable.requsetAssociatedPageData)
        // debugger
        if (data.Code.toString() === '0') {
          console.log('getAllocatedCourseByPageList success')
          this.dialogTable.allocatedProductTable.tableData = data.Data.ReusltList
          this.dialogTable.allocatedProductTable.pageTotal = data.Data.RecordCount
        } else {
          console.log('关联产品无数据')
          this.common.toast('关联产品无数据')
        }
      } catch (e) {
        console.log(e)
      }
      Loading.hideLoading()
    }
  }
}
</script>

<style lang="scss" scoped>
  .dashboard {
    &-container {
      margin: 30px;
    }
    &-text {
      font-size: 30px;
      line-height: 46px;
    }
  }
  .p_title{
    border-left: 3px solid red;
    padding-left: 5px;
    margin-top: 10px;
    color: red;
    margin-bottom: 10px;
  }

  @media only screen and (max-width: 1508px) {
    .productCourseBox >>> .el-form-item__label{
      width: 100%;
      text-align: left;
    }
    .marginTop {
      margin-top: 40px;
    }
  }
</style>

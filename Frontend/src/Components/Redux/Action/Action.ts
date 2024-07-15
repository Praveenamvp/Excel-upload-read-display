import Excel from "../../../Models/Excel";
import ExcelRequest from "../../../Models/ExcelRequest";
export const UPDATE_EXCELREQUEST="UPDATE_EXCELREQUEST";
export const UPDATE_INITIALEXCELREQUEST="UPDATE_INITIALEXCELREQUEST";
export const UPDATE_EXCELDATA="UPDATE_EXCELDATA";
export const updateInitialExcelRequest=(excelInitialData:any[])=>{
    return{
    type:UPDATE_INITIALEXCELREQUEST,
    payload:excelInitialData
    }
}

export const updateExcelRequest=(excelData:ExcelRequest[])=>{
    return{
    type:UPDATE_EXCELREQUEST,
    payload:excelData
    }
}
export const updateExcelData=(excelDatas:Excel[])=>{
    return{
    type:UPDATE_EXCELDATA,
    payload:excelDatas
    }
}

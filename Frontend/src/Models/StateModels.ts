import Excel from "./Excel";
import ExcelRequest from "./ExcelRequest";

export default interface ExcelDetails{
    excelRequest:ExcelRequest[];
    intialExcelRequest:any[];
    excelDatas:Excel[];

}
export interface Datas {
    nodes : ExcelDetails,
  }
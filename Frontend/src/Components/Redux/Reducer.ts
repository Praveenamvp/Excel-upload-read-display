import ExcelDetails from "../../Models/StateModels";
import {
  UPDATE_EXCELREQUEST,
  UPDATE_INITIALEXCELREQUEST,
  UPDATE_EXCELDATA
} from "./Action/Action";

const intialState: ExcelDetails = {
  excelRequest: [],
  intialExcelRequest: [],
  excelDatas:[]
};

export const excelReducer = (state = intialState, action: any) => {
  switch (action.type) {
    case UPDATE_INITIALEXCELREQUEST:
        console.log(action.payload,"action.payload");
        
      return {
        ...state,
        intialExcelRequest: action.payload,
      };
    case UPDATE_EXCELREQUEST:
      return {
        ...state,
        excelRequest: action.payload,
      };
      case UPDATE_EXCELDATA:
      return {
        ...state,
        excelDatas: action.payload,
      };
      default:
        return state;
  }
 
};

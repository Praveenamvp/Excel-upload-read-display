import { useEffect } from "react";
import * as XLSX from "xlsx";
import "../DataUpload/DataUpload.css";
import { useDispatch, useSelector } from "react-redux";
import {
  updateExcelData,
  updateExcelRequest,
  updateInitialExcelRequest,
} from "../Redux/Action/Action";

import ExcelService from "../../Services/ExcelService";
import Excel from "../../Models/Excel";

function DataUpload() {
  const dispatch = useDispatch();
  const excelDetails = useSelector((state: any) => state);
  useEffect(() => {
    fetchExcelData();
  }, []);
  useEffect(() => {
    console.log(excelDetails.intialExcelRequest, "data from reducer");
    dispatch(updateExcelRequest(excelDetails.intialExcelRequest));
  }, [excelDetails.intialExcelRequest]);

  useEffect(() => {
    if (excelDetails.excelRequest.length > 0) {
      console.log(excelDetails.excelRequest, "data from reducer excel");
      addExcelDatas();
      fetchExcelData();
    }
  }, [excelDetails.excelRequest]);

  let addExcelDatas = async () => {
    var result = await ExcelService.addData(excelDetails.excelRequest);
    console.log(result?.status);
    fetchExcelData();
  };
  let fetchExcelData = async () => {
    var result = await ExcelService.fetchData();
    dispatch(updateExcelData(result.data));
    console.log(excelDetails.excelDatas);
  };

  const handleInput = (e: any) => {
    const file = e.target.files[0];

    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        const data = new Uint8Array(e.target.result);
        const workbook = XLSX.read(data, { type: "array" });
        const sheetName = workbook.SheetNames[0];
        const sheet = workbook.Sheets[sheetName];
        const jsonData = XLSX.utils.sheet_to_json(sheet);
        console.log(jsonData);

        dispatch(updateInitialExcelRequest(jsonData));
      };

      reader.readAsArrayBuffer(file);
    }
  };

  return (
    <div>
      <h2>Upload Data</h2>
      <div className="upload-data">
        <input type="file" onChange={handleInput}></input>
      </div>

      <br />
      {excelDetails.excelDatas.length > 0 ? (
        <div>
          {" "}
          <table>
            <tr>
              <th>Feature ID</th>
              <th>Name</th>
              <th>Description</th>
              <th>Version</th>
            </tr>

            {excelDetails.excelDatas.map((excelD: Excel) => (
              <tr>
                <td>{excelD.featureID}</td>
                <td>{excelD.name}</td>
                <td>{excelD.description}</td>
                <td>{excelD.versionNo}</td>
              </tr>
            ))}
          </table>
        </div>
      ) : (
        <h1>Oops there is no data currently</h1>
      )}
    </div>
  );
}

export default DataUpload;

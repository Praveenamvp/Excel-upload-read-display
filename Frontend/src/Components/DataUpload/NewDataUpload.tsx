import React, { useEffect, useState } from "react";
import "../DataUpload/DataUpload.css";
import { useDispatch, useSelector } from "react-redux";
import ExcelService from "../../Services/ExcelService";
import { updateExcelData } from "../Redux/Action/Action";
import Excel from "../../Models/Excel";

function NewDataUpload() {
  const [selectedFile, setSelectedFile] = useState<any>();
  const dispatch = useDispatch();

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    setSelectedFile(file);
  };
  useEffect(() => {
    fetchExcelData();
  }, []);
  useEffect(() => {
    console.log(selectedFile);
    
    addExcelDatas();
  }, [selectedFile]);
  const excelDetails = useSelector((state: any) => state);

  const addExcelDatas = async () => {
    if (selectedFile) {
      const result = await ExcelService.newAddData(selectedFile);
      console.log(result?.status);
      fetchExcelData();
    }
  };
  let fetchExcelData = async () => {
    var result = await ExcelService.fetchData();
    dispatch(updateExcelData(result.data));
  };

  return (
    <div>
      <h2>Upload Data</h2>
      <div className="upload-data">
        <input type="file" id="fileInput" onChange={handleFileChange}></input>
      </div>

      <br />
      {excelDetails.excelDatas.length > 0 ? (
        <div>
          <table>
            <thead>
              <tr>
                <th>Feature ID</th>
                <th>Name</th>
                <th>Description</th>
                <th>Version</th>
              </tr>
            </thead>
            <tbody>
              {excelDetails.excelDatas.map((excelD: Excel) => (
                <tr key={excelD.featureID}>
                  <td>{excelD.featureID}</td>
                  <td>{excelD.name}</td>
                  <td>{excelD.description}</td>
                  <td>{excelD.versionNo}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ) : (
        <h1>Oops, there is no data currently</h1>
      )}
    </div>
  );
}

export default NewDataUpload;

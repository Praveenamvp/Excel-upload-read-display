import  { useEffect } from "react";
import { useDropzone } from "react-dropzone";
import ExcelService from "../../Services/ExcelService";
import { useDispatch, useSelector } from "react-redux";
import { updateExcelData } from "../Redux/Action/Action";
import "../DataUpload/DataUpload.css";
import { IoCloudUploadOutline } from "react-icons/io5";

function Basic() {
  let data=new FormData();

  const { acceptedFiles, getRootProps, getInputProps } = useDropzone({
    onDrop: (files) => {
        data.append("file",acceptedFiles[0])
        addExcelDatas(files[0]);
        fetchExcelData();
    },
  });
  const dispatch = useDispatch();


  useEffect(() => {
    fetchExcelData();
  }, []);
  const addExcelDatas = async (file:any) => {
    
      const result = await ExcelService.newAddData(file);
      console.log(result?.status);
      fetchExcelData();
    
  };
  let fetchExcelData = async () => {
    var result = await ExcelService.fetchData();
    dispatch(updateExcelData(result.data));
  };

  const excelDetails = useSelector((state: any) => state);

  return (
    <section className="container">
      <div className= "upload-data">
      <div>
        <i className="upload-icon"><IoCloudUploadOutline /></i><br/>
        Drag and drop file here
        (accepted files .xlsx)
        </div>
        <span>or</span>
      <div {...getRootProps({  })}className="upload-browse">
       
      <input {...getInputProps()}  />
       Browse File
      </div>
      </div>
      <br/>
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

            {excelDetails.excelDatas.map((excelD: any) => (
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
    </section>
  );
}

export default Basic;

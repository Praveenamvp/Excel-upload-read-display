import axios from "axios";
import ExcelRequest from "../Models/ExcelRequest";
class ExcelService {
  http = axios.create({
    baseURL: "https://localhost:7139/api/Excel/",
  });
  async addData(data: ExcelRequest[]) {
    const response = await this.http.post("AddExcelData", data);
    return response;
  }
  async newAddData(file: any) {
    
    const formData = new FormData();
    formData.append("excelFile", file);
    console.log(formData+"data");
    // https://localhost:7139/api/Excel/NewAddExcelData

    try {
      const response = await this.http.post("NewAddExcelData", formData)
      return response;
    } catch (error) {
      console.error("Error adding Excel data:", error);
      throw error;
    }
  }
  async fetchData() {
    const response = await this.http.get("GetAllExcelData");
    return response;
  }
}
export default new ExcelService();

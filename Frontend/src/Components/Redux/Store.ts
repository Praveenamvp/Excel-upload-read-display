import { createStore } from "redux";
import { excelReducer } from "./Reducer";

export const store=createStore(excelReducer)
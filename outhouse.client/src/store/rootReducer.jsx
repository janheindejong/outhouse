import { combineReducers } from "@reduxjs/toolkit";
import appState from "./appState/reducer";

const rootReducer = combineReducers({
  appState,
});

export default rootReducer;

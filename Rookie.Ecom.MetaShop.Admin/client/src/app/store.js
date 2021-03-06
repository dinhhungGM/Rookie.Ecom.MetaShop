import { configureStore } from '@reduxjs/toolkit'
import CategoryReducer from '../features/categorySlice';
import ProductReducer from '../features/productSlice';
import UserReducer from '../features/userSlice';
import OverviewReducer from '../features/overviewSlice';
import { getDefaultMiddleware } from '@reduxjs/toolkit';
import createOidcMiddleware from "redux-oidc";
import userManager from "../utils/userManager";
import { reducer as oidc } from "redux-oidc";


export const store = configureStore({
  reducer: {
    category: CategoryReducer,
    product: ProductReducer,
    user: UserReducer,
    oidc: oidc,
    overview: OverviewReducer
  },
  middleware: [...getDefaultMiddleware({
    serializableCheck: false
  }), (createOidcMiddleware(userManager))]
})
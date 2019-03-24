
import {ModuleWithProviders} from "@angular/core"
import {RouterModule, Routes} from "@angular/router";


export const routes:Routes = [

  {
    path: 'normal',
    loadChildren: './normal-tables/normal-tables.module#NormalTablesModule',
    data: {pageTitle: 'Normal'}
  },

  {
    path: 'datatables',
    loadChildren: './datatables-case/datatables-case.module#DatatablesCaseModule',
    data: {pageTitle: 'Datatables'}
  },

  {
    path: 'ngx-datatable',
    loadChildren: './ngx-datatable/ngx-datatable-case.module#NgxDatatableCaseModule',
    data: {pageTitle: 'NGx Datatable'}
  }
];


export const routing = RouterModule.forChild(routes)

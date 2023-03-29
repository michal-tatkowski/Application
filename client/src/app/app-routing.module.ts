import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { SharedComponentRouts } from "./shared/configs/component-routes.config";

const routes: Routes = [
    { path: '', redirectTo: SharedComponentRouts.login, pathMatch: 'full'},
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {

}

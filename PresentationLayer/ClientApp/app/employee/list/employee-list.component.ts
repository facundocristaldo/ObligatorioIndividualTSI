import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../employee.service';
import { Http, Response } from '@angular/http';
import { EmployeeModel } from '../../EmployeeModel';

@Component({
    selector: 'tsi1-employee-list',
    templateUrl: 'employee-list.component.html',
    styleUrls: [ 'employee-list.component.css']
})
export class EmployeeListComponent {

    tipoEmpSel: string = "0";
    fechaEmpSel: Date | undefined;
    nombreEmpSel: string = "";
    idEmpSel: number = 0;

    array2: EmployeeModel[] = [];
    constructor(public employeeService: EmployeeService) {

    }

    public cargarEmpleados() {
        
        this.employeeService.getEmployees(this.idEmpSel, this.nombreEmpSel, this.tipoEmpSel, this.fechaEmpSel).subscribe(
            (res: Response) => {
                const array = res.json();
                this.array2 = [];
                console.log(res.json());
                for (let a in array) {

                    if (array[a].Type == 1) {
                        this.array2.push(new EmployeeModel(array[a].Id, array[a].Name, array[a].Type, array[a].StartDate, array[a].HourlyRate));

                    } else {
                        this.array2.push(new EmployeeModel(array[a].Id, array[a].Name, array[a].Type, array[a].StartDate, array[a].Salary));
                    }
                    //console.log(this.array2[a]);

                }
                
            }

        );
    }

    delay(ms: number) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }


    delete(id: number) {
        this.employeeService.deleteEmployee(id);
        this.ngOnInit();
    }

    async ngOnInit() {

        this.array2 = [];
        await this.delay(300);
        this.cargarEmpleados();
    }





}

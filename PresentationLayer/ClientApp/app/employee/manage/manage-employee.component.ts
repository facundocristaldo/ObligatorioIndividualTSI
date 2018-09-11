import { Component } from '@angular/core';
import { EmployeeService } from '../employee.service';

@Component({
    selector: 'tsi1-manage-employee',
    templateUrl: 'manage-employee.component.html',
    styleUrls: [ 'manage-employee.component.css']
})
export class ManageEmployeeComponent {

    Name: string = "";
    Type: number = 1;
    Value: number = 0;
    StartDate: Date | undefined;

    constructor(public employeeservice: EmployeeService) {
        this.Type = 1;
        this.Name = "";
        this.Value = 0;
        this.StartDate ;

    }
    

    addEmployee(): boolean {
        console.log(this.Name, this.Type, this.StartDate, this.Value);
        this.employeeservice.addEmployee(this.Name, this.Type, this.StartDate, this.Value).subscribe(
            (res: Response) => {
                return res.json();

            }
        );
        return false;
    }
}


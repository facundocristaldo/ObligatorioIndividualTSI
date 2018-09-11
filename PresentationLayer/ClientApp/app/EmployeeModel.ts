

export class EmployeeModel {
    Id: number = 0;
    Name: String = "";
    Type: number = 0;
    Salary: number = 0;
    HourlyRate: number = 0;
    StartDate: Date | undefined;



    constructor(Id: number, name: string, type: number, startdate: Date | undefined, value: number) {
        this.Id = Id;
        this.Name = name;
        this.StartDate = startdate;

        this.Type = type;
        if (type == 1) { //type 1 == ParttimeEmployee
            this.HourlyRate = value;

        } else {
            this.Salary = value;
        }
    }

    tostring(): string {
        if (this.Type == 1) {
            return "Emp: " + this.Id + " with name " + this.Name + " started on " + this.StartDate + " and is a part time employee with a hourly rate of " + this.HourlyRate;
        } else {
            return "Emp: " + this.Id + " with name " + this.Name + " started on " + this.StartDate + " and is a full time employee with a salary of " + this.Salary;
        }
    }
}
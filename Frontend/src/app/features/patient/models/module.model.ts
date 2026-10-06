export interface Module {
  moduleID: number;
  moduleCode: string;
  moduleName: string;
  route: string | null;
  isActive: boolean;
}
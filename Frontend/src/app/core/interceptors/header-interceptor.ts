import { HttpInterceptorFn } from '@angular/common/http';

export const headerInterceptor: HttpInterceptorFn = (req, next) => {
  const clonedRequest = req.clone({
    setHeaders: {
      HospitalID: '1',
      LocationID: '1',
      UserID: '1'
    }
  });

  return next(clonedRequest);
};
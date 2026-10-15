_QWORD *OCL_2()
{
  _QWORD *result; // rax

  result = (_QWORD *)(sub_18002D230() + 200);
  if ( result[3] >= 0x10u )
    return (_QWORD *)*result;
  return result;
}

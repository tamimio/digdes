% detect lines on *.png image, count them and their length
prompt = 'Input path -> ';
img_path = input(prompt, 's');
if exist(img_path, 'file') == 2
    A = imread(img_path);
else
    A = imread('test.png');
end

hor_line=0;
len_h=zeros(1,100); 
for (j=1:size(A,1)) 
	for (i=2:size(A,2)-1)
        if (A(j,i)~=255 && A(j,i-1)~=A(j,i) && A(j,i+1)==A(j,i))
            % if not background && not a single dot
            hor_line=hor_line+1;
            k=i;
            while (A(j,k)~=255) 
                len_h(hor_line)=len_h(hor_line)+1;
                k=k+1;
            end
        end
          
    end
end
len_h(:,hor_line+1:end)=[];

vert_line=0;
len_v=zeros(1,100);
for (j=1:size(A,2)) 
	for (i=2:size(A,1)-1) 
        if (A(i,j)~=255 && A(i-1,j)~=A(i,j) && A(i+1,j)==A(i,j))
            % if not background && not a single dot
            vert_line=vert_line+1;
            k=i;
            while (A(k,j)~=255) 
                len_v(vert_line)=len_v(vert_line)+1;
                k=k+1;
            end
        end
          
    end
end
len_v(:,vert_line+1:end)=[];

fprintf('Number of lines: %d\n', vert_line+hor_line);
fprintf('Number of horizontal lines: %d\n', hor_line);
fprintf('Their length: ');
fprintf('%d ', len_h);
fprintf('\nNumber of vertical lines: %d\n', vert_line);
fprintf('Their length: ');
fprintf('%d ', len_v);
fprintf('\n');